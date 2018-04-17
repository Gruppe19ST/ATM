using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;
using TransponderReceiver;

namespace ATM.Receiver
{
    public class TrackReceiver : ITrackReceiver
    {
        private List<TrackObject> listOfTrackObjects;
        private ITrackConverter trackConverter;

        public TrackReceiver(TrackConverter converter)
        {
            trackConverter = converter;
            //trackConverter = new TrackConverter(TransponderReceiverFactory.CreateTransponderDataReceiver());
            trackConverter.TrackObjectsReady += TrackConverter_TrackObjectsReady;

            listOfTrackObjects = new List<TrackObject>();
        }

        public void TrackConverter_TrackObjectsReady(object sender, TrackObjectEventArgs e)
        {
            listOfTrackObjects = e.TrackObjects;
            foreach (var track in listOfTrackObjects)
            {
                Console.WriteLine(track.ToString());
            }
        }

        public List<TrackObject> GetListOfTrackObjects()
        {
            return listOfTrackObjects;
        }
    }
}
