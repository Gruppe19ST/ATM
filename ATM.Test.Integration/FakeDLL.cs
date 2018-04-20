using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ATM.Test.Integration
{
    public class FakeDLL 
    {
        List<TrackObject> _faketracks1;
        List<TrackObject> _faketracks2;
        TrackObject t1, t2, t3, t4, t5, t6;


        public FakeDLL()
        {
            _faketracks1 = new List<TrackObject>();
            _faketracks2 = new List<TrackObject>();
            t1 = new TrackObject("Fly1", 88000, 88000, 6000, DateTime.ParseExact("20180420222222222","yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            t2 = new TrackObject("Fly2", 72000, 91000, 19999, DateTime.ParseExact("20180420222222222", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            t3 = new TrackObject("Fly3", 86000, 86000, 6500, DateTime.ParseExact("20180420222222222", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            t4 = new TrackObject("Fly3", 84000, 84000, 6502, DateTime.ParseExact("20180420222222222", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            t5 = new TrackObject("Fly4", 40000, 32000, 10000, DateTime.ParseExact("20180420222222222", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            t6 = new TrackObject("Fly2", 74000, 74000, 10000, DateTime.ParseExact("20180420222222222", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));

            _faketracks1.Add(t1);
            _faketracks1.Add(t2);
            _faketracks1.Add(t3);
            _faketracks2.Add(t4);
            _faketracks2.Add(t5);
            _faketracks2.Add(t6);
        }

    }
}
