using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using System.Globalization;
using ATM.Logic.Handlers;
using ATM.Logic.Interfaces;

namespace ATM
{
    public class TrackConverter : ITrackConverter
    {
        private ITransponderReceiver _transponderReceiver;
        private List<TrackObject> listOfTrackObjects;

        public TrackConverter(ITransponderReceiver transponderReceiver)
        {
            _transponderReceiver = transponderReceiver;
            // Register when a list of raw transponder data is ready 
            _transponderReceiver.TransponderDataReady += _transponderReceiver_TransponderDataReady;

            listOfTrackObjects = new List<TrackObject>();
        }

        private void _transponderReceiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            listOfTrackObjects.Clear();
            // Convert all raw data entries to a track object
            foreach (var data in e.TransponderData) 
            {
                TrackObject track = ConvertTrackObject(data); 
                listOfTrackObjects.Add(track);
            }
            // Raise an event with a list of track objects 
            OnTrackObjectListUpdated(new TrackObjectEventArgs(listOfTrackObjects));
        }

        public void OnTrackObjectListUpdated(TrackObjectEventArgs trackObjects)
        {
            var handler = TrackObjectsReady;
            handler?.Invoke(this,trackObjects);
        }

        public TrackObject ConvertTrackObject(string responderData)
        {
            // split the string with raw data into four elements, then create a track object from those elemente
            string[] elements = responderData.Split(';');
            string tag = elements[0];
            float xCoordinate = float.Parse(elements[1]);
            float yCoordinate = float.Parse(elements[2]);
            float altitude = float.Parse(elements[3]);
            DateTime timeStamp = DateTime.ParseExact(elements[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            return new TrackObject(tag, xCoordinate, yCoordinate, altitude, timeStamp); // returnerer nyt track
        }

        public event EventHandler<TrackObjectEventArgs> TrackObjectsReady;
    }
}
