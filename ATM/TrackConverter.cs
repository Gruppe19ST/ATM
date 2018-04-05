using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using System.Globalization;

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

        private void _transponderReceiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData) // liste af strings med rå trackdata  
            {
                TrackObject track = ConvertTrackObject(data); // Opretter et nyt track for hvert element i listen
                Console.WriteLine(track.ToString()); // udskriver track i konsolvinduet 
            }
        }

        public TrackObject ConvertTrackObject(string responderData)
        {
            string[] elements = responderData.Split(';'); // delimiter er ; 
            string tag = elements[0];
            float xCoordinate = float.Parse(elements[1]);
            float yCoordinate = float.Parse(elements[2]);
            float altitude = float.Parse(elements[3]);
            // string timeStamp = elements[4]; // skal ændres til en datetime senere, så output bliver læseligt. 
            DateTime timeStamp = DateTime.ParseExact(elements[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            // https://msdn.microsoft.com/en-us/library/w2sa9yss(v=vs.110).aspx
            return new TrackObject(tag, xCoordinate, yCoordinate, altitude, timeStamp); // returnerer nyt track
        }

    }
}
