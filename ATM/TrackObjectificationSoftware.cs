using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    public class TrackObjectificationSoftware
    {
        //private ITransponderReceiver myReceiver;
        //private List<TrackObject> tracks;

        //public TrackObjectificationSoftware(ITransponderReceiver receiver)
        //{
        //    myReceiver = receiver;
        //    myReceiver.TransponderDataReady += MyReceiver_TransponderDataReady;
        //}

        //private void MyReceiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        //{
        //    var receiverData = e.TransponderData;
        //    convertData(receiverData);
        //}

        //private void convertData(List<String> receiverData)
        //{
        //    tracks = new List<TrackObject>();
        //    TrackObject trackObject = new TrackObject();

        //    foreach (var track in receiverData)
        //    {
        //        string[] elements = track.Split(';');
        //        trackObject.Tag = elements[0];
        //        trackObject.XCoordinate = float.Parse(elements[1]);
        //        trackObject.YCoordinate = float.Parse(elements[2]);
        //        trackObject.Altitude = float.Parse(elements[3]);
        //        trackObject.TimeStamp = elements[4];
        //        tracks.Add(trackObject);
        //    }
            
        //}

        //public List<TrackObject> getTracks()
        //{
        //    return tracks;
        //}
    }
}
