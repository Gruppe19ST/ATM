using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class TrackConverterUnitTest
    {

        private TrackConverter _uut;
        private ITransponderReceiver _transponderReceiver;
        

        string input1 = "Tag123;12345;12345;1000;20151006213456789";
        string input2 = "Tag456;45678;45678;2000;20170904133850452";

        private List<string> info;
        private int _nEventsRaised;
        private TrackObjectEventArgs _receivedArgs;
        

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TrackConverter(_transponderReceiver);

            info = new List<string>();
            info.Add(input1);
            info.Add(input2);

            _nEventsRaised = 0;
            
            _uut.TrackObjectsReady += (o, args) => {
                ++_nEventsRaised;
                _receivedArgs = args;
               
            };

        }

        [Test]
        public void RaiseEvent_ListWith2_Raised1Event()
        {
            var args = new RawTransponderDataEventArgs(info);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            Assert.That(_nEventsRaised, Is.EqualTo(1));
            

        }

        [Test]
        public void RaiseEvent_ListWith2_Correct1stTrackTag()
        {

            var args = new RawTransponderDataEventArgs(info);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            Assert.That(_receivedArgs.TrackObjects[0].Tag, Is.EqualTo("Tag123"));
        }

        [Test]
        public void RaiseEvent_ListWith2_Correct2ndTrackAltitude()
        {

            var args = new RawTransponderDataEventArgs(info);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            Assert.That(_receivedArgs.TrackObjects[1].Altitude, Is.EqualTo(2000));
        }

        [Test]
        public void RaiseEvent_ListWith2_Correct1stTrackXcor()
        {

            var args = new RawTransponderDataEventArgs(info);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            Assert.That(_receivedArgs.TrackObjects[0].XCoordinate, Is.EqualTo(12345));
        }

        [Test]
        public void RaiseEvent_ListWith2_Correct2ndTrackYcor()
        {

            var args = new RawTransponderDataEventArgs(info);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            Assert.That(_receivedArgs.TrackObjects[1].YCoordinate, Is.EqualTo(45678));
        }

        [Test]
        public void RaiseEvent_ListWith2_Correct1stTrackTimeStamp()
        {

            var args = new RawTransponderDataEventArgs(info);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            Assert.That(_receivedArgs.TrackObjects[0].TimeStamp, Is.EqualTo(DateTime.ParseExact("20151006213456789", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));
        }
    }
}
