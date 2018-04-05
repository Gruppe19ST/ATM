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
        public float XCoordinate;
        public float YCoordinate;
        public float Altitude;
        public string TimeStamp;

        public TrackObject()
        {
            
        }

    }
}
