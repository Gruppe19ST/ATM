using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    public class TrackConverter
    {
        private ITransponderReceiver _transponderReceiver;

        public TrackConverter(ITransponderReceiver transponderReceiver)
        {
            _transponderReceiver = transponderReceiver;
            _transponderReceiver.TransponderDataReady += _transponderReceiver_TransponderDataReady;
        }

        public TrackObject ConvertTrackObject(string responderData)
        {
            string[] elements = responderData.Split(';');
            string tag = elements[0];
            float xCoordinate = float.Parse(elements[1]);
            float yCoordinate = float.Parse(elements[2]);
            float altitude = float.Parse(elements[3]);
            string timeStamp = elements[4]; // skal ændres til en datetime senere, så output bliver læseligt. 
            return new TrackObject(tag, xCoordinate, yCoordinate, altitude, timeStamp);
        }

        private void _transponderReceiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                TrackObject track = ConvertTrackObject(data);
                Console.WriteLine(track.ToString());
            }
        }
    }
}
