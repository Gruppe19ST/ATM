using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ATM.Test.Integration
{
    public class FakeDLL : ITransponderReceiver
    {
        List<string> _faketracks1;
        List<string> _faketracks2;
        string t1, t2, t3, t4, t5, t6;


        public FakeDLL()
        {
            _faketracks1 = new List<string>();
            _faketracks2 = new List<string>();
            t1 = "Fly1;88000;88000;6000;20180420222222222";
            t2 = "Fly2;72000;91000;19999;20180420222222222";
            t3 = "Fly3;86000;86000;6500;20180420222222222";
            t4 = "Fly3;84000;84000;6502;20180420222222222";
            t5 = "Fly4;40000;32000;10000;20180420222222222";
            t6 = "Fly2;74000;74000;10000;20180420222222222";

            _faketracks1.Add(t1);
            _faketracks1.Add(t2);
            _faketracks1.Add(t3);
            _faketracks2.Add(t4);
            _faketracks2.Add(t5);
            _faketracks2.Add(t6);
            OnTransponderDataReady(new RawTransponderDataEventArgs(_faketracks1));

        }

        public void OnTransponderDataReady(RawTransponderDataEventArgs rawTracks)
        {
            var handler = TransponderDataReady;
            handler?.Invoke(this, rawTracks);
        }
        public event EventHandler<RawTransponderDataEventArgs> TransponderDataReady;
    }
}
