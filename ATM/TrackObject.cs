using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    public class TrackObject
    {
        public string Tag { get; set; }
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public double horizontalVelocity { get; set; }
        public double compassCourse { get; set; }

        // Default constructor for unit test
        public TrackObject()
        {
        }

        public TrackObject(string tag, float x, float y, float alt, DateTime time)
        {
            Tag = tag;
            XCoordinate = x;
            YCoordinate = y;
            Altitude = alt;
            TimeStamp = time;
            horizontalVelocity = 0;
            compassCourse = 0;
        }

        public override string ToString()
        {
            String trackInfo=String.Format("Track Tag: "+Tag+": X coordinates: " +XCoordinate+", Y coordinates: "+YCoordinate+", Altitude: "+Altitude+"m, Horizontal velocity: " + horizontalVelocity +"m/s, Compass course: "+ compassCourse+"deg, Timestamp: "+TimeStamp);
            return trackInfo;
        }
    }
}
