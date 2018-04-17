using System;
using System.Collections.Generic;
using System.Globalization;
using TransponderReceiver;

namespace ATM.Logic.Handlers.Converter
{
    public class TrackConverter : ITrackConverter
    {
        private ITransponderReceiver _transponderReceiver;
        private List<TrackObject> listOfTrackObjects;

        public TrackConverter(ITransponderReceiver transponderReceiver)
        {
            _transponderReceiver = transponderReceiver;
            _transponderReceiver.TransponderDataReady += _transponderReceiver_TransponderDataReady;
            listOfTrackObjects = new List<TrackObject>();
        }

        private void _transponderReceiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            listOfTrackObjects.Clear();
            foreach (var data in e.TransponderData) // liste af strings med rå trackdata  
            {
                TrackObject track = ConvertTrackObject(data); // Opretter et nyt track for hvert element i listen
                listOfTrackObjects.Add(track);
                //Console.WriteLine(track.ToString()); // udskriver track i konsolvinduet 

                // Add til en liste af Tracks? Så vi kan på et tidspunkt kan holde styr på det enkelte? 
            }

            // Raise et event med listen
            OnTrackObjectListUpdated(new TrackObjectEventArgs(listOfTrackObjects));
            

        }

        public void OnTrackObjectListUpdated(TrackObjectEventArgs trackObjects)
        {
            var handler = TrackObjectsReady;
            handler?.Invoke(this,trackObjects);

        }

        public TrackObject ConvertTrackObject(string responderData)
        {
            string[] elements = responderData.Split(';'); // delimiter er ; 
            string tag = elements[0];
            float xCoordinate = float.Parse(elements[1]);
            float yCoordinate = float.Parse(elements[2]);
            float altitude = float.Parse(elements[3]);
            DateTime timeStamp = DateTime.ParseExact(elements[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            // https://msdn.microsoft.com/en-us/library/w2sa9yss(v=vs.110).aspx
            return new TrackObject(tag, xCoordinate, yCoordinate, altitude, timeStamp); // returnerer nyt track
        }

        public event EventHandler<TrackObjectEventArgs> TrackObjectsReady;
    }
}
