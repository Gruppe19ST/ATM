using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class AtmUnitTest
    {
        private TrackConverter _uut;
        private ITransponderReceiver _transponderReceiver;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TrackConverter(_transponderReceiver);
        }

        [Test]
        public void stringToTrack_Conversion_TagIsCorrect()
        {
            string input = "Tag123;12345;12345;1000;20151006213456789";
            TrackObject outputTrack = _uut.ConvertTrackObject(input);
            Assert.That(outputTrack.Tag,Is.EqualTo("Tag123"));
        }

        [Test]
        public void stringToTrack_Conversion_XcorIsCorrect()
        {
            string input = "Tag123;12345;12345;1000;20151006213456789";
            TrackObject outputTrack = _uut.ConvertTrackObject(input);
            Assert.That(outputTrack.XCoordinate, Is.EqualTo(12345));
        }
        [Test]
        public void stringToTrack_Conversion_YcorIsCorrect()
        {
            string input = "Tag123;12345;12345;1000;20151006213456789";
            TrackObject outputTrack = _uut.ConvertTrackObject(input);
            Assert.That(outputTrack.YCoordinate, Is.EqualTo(12345));
        }
        [Test]
        public void stringToTrack_Conversion_AltIsCorrect()
        {
            string input = "Tag123;12345;12345;1000;20151006213456789";
            TrackObject outputTrack = _uut.ConvertTrackObject(input);
            Assert.That(outputTrack.Altitude, Is.EqualTo(1000));
        }

        [Test]
        public void stringToTrack_Conversion_TimestampIsCorrect()
        {
            string input = "Tag123;12345;12345;1000;20151006213456789";
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



    }
}
