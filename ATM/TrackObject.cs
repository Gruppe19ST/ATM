﻿using System;
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
        public string TimeStamp { get; set; }

        public TrackObject(string tag, float x, float y, float alt, string time)
        {
            Tag = tag;
            XCoordinate = x;
            YCoordinate = y;
            Altitude = alt;
            TimeStamp = time;
        }

        public override string ToString()
        {
            String trackInfo=String.Format("Track Tag: "+Tag+" X coordinates: " +XCoordinate+" Y coordinates: "+YCoordinate+" Altitude: "+Altitude+"m Timestamp: "+TimeStamp);
            return trackInfo;
        }
    }
}