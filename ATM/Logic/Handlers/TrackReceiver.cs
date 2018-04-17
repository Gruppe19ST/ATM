using System;
using System.Collections.Generic;
using ATM.Logic.Handlers.Converter;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
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
