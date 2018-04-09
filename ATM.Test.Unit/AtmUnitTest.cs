using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Receiver;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class AtmUnitTest
    {
        private TrackConverter _uut;
        private ITransponderReceiver _transponderReceiver;
        private ITrackReceiver _trackReceiver;
        string input1 = "Tag123;12345;12345;1000;20151006213456789";
        string input2 = "Tag456;45678;45678;2000;20170904133850452";
        private List<string> info;
        private int _nEventsRaised, _nEventsReceived;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TrackConverter(_transponderReceiver);
            info = new List<string>();
            info.Add(input1);
            info.Add(input2);
            _nEventsRaised = 0;
            _nEventsReceived = 0;

            _trackReceiver = Substitute.For<ITrackReceiver>();

            _transponderReceiver.TransponderDataReady += (o, args) => { ++_nEventsReceived; };
            _uut.TrackObjectsReady += (o, args) => { ++_nEventsRaised; };
            

            

        }

        [Test]
        public void attachToEvent_ListWith2_Received1Event()
        {
            var args = new RawTransponderDataEventArgs(info);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            Assert.That(_nEventsReceived, Is.EqualTo(1));

        }

        [Test]
        public void attachToEvent_ListWith2_Raised1Event()
        {
            var args = new RawTransponderDataEventArgs(info);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            Assert.That(_nEventsRaised, Is.EqualTo(1));

        }

        /* [Test]
         public void handleEventData_ListWith2_2TrackObjects()
         {
             var args = new RawTransponderDataEventArgs(info);

             _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
             _trackReceiver.Received()
             _trackReceiver.Received().TrackConverter_TrackObjectsReady(object sender, TrackObjectEventArgs eventArgs);
             Assert.That(_uut.Received().OnTrackObjectListUpdated(), Is.True);
             Assert.That(_trackReceiver.GetListOfTrackObjects().Count, Is.EqualTo(2));
         }*/

        /*[Test]
        public void handleEventData_ListWith2_CorrectTag()
        {
            var args = new RawTransponderDataEventArgs(info);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            Assert.That(_trackReceiver.GetListOfTrackObjects().ElementAt(0).Tag, Is.EqualTo("Tag123"));
        }*/

       /* [Test]
        public void handleEventData_ListWith2_RaiseEvent()
        {
            var args = new RawTransponderDataEventArgs(info);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            _trackReceiver.Received().TrackConverter_TrackObjectsReady(this,);


        }*/

        /*[Test]
        public void stringToTrack_Conversion_TagIsCorrect()
        {
            TrackObject outputTrack = _uut.ConvertTrackObject(input);
            Assert.That(outputTrack.Tag,Is.EqualTo("Tag123"));
        }

        [Test]
        public void stringToTrack_Conversion_XcorIsCorrect()
        {
            TrackObject outputTrack = _uut.ConvertTrackObject(input);
            Assert.That(outputTrack.XCoordinate, Is.EqualTo(12345));
        }

        [Test]
        public void stringToTrack_Conversion_YcorIsCorrect()
        {
            TrackObject outputTrack = _uut.ConvertTrackObject(input);
            Assert.That(outputTrack.YCoordinate, Is.EqualTo(12345));
        }

        [Test]
        public void stringToTrack_Conversion_AltIsCorrect()
        {
            TrackObject outputTrack = _uut.ConvertTrackObject(input);
            Assert.That(outputTrack.Altitude, Is.EqualTo(1000));
        }

        [Test]
        public void stringToTrack_Conversion_TimestampIsCorrect()
        {
            TrackObject outputTrack = _uut.ConvertTrackObject(input);
            Assert.That(outputTrack.TimeStamp, Is.EqualTo(DateTime.ParseExact("20151006213456789", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));
        }




        //[Test] // Fra Emma
        //public void String_to_Track()
        //{
        //    List<String> trackInfo = new List<string> {"Tag123;12345;12345;1000;20151006213456789"};

        //    TrackObject track = new TrackObject()//mangler parametre
        //    {
        //        Tag = "Tag123",
        //        XCoordinate = 12345,
        //        YCoordinate = 12345,
        //        Altitude = 1000,
        //        TimeStamp = new DateTime(2015, 10, 06, 21, 34, 56, 789)
        //    };

        //    _uut.ConvertTrackObject(trackInfo);
        //    Assert.That(_uut.Tracks[0], is.TypeOf<TrackObject>());
        //}
        */


    }
}
