using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using NSubstitute;
using ATM;

namespace ATM.Test.Unit
{
    [TestFixture]
    class TrackCourseUnitTest
    {
        private ATM.Logic.Handlers.TrackCompassCourse _uut;
        private TrackObject track1;
        private TrackObject track2;
        private double trackcoure;

        [SetUp]

        public void Setup()
        {
            _uut = new ATM.Logic.Handlers.TrackCompassCourse();
            trackcoure = 0;

        }

        [Test]
        public void CalculateCoursePositiveXandY()
        {
            track1 = new TrackObject("Tag123", 80000, 60000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            track2 = new TrackObject("Tag123", 80500, 60400, 1000, DateTime.ParseExact("20180412111322111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            trackcoure = _uut.CalculateCompassCourse(track1, track2);

            Assert.That(Math.Round(trackcoure), Is.EqualTo(Math.Round(51.0)));
        }

        [Test]
        public void CalculateCourseNegativeXandY()
        {
            track1 = new TrackObject("Tag123", 80000, 60000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            track2 = new TrackObject("Tag123", 70500, 50400, 1000, DateTime.ParseExact("20180412111322111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            trackcoure = _uut.CalculateCompassCourse(track1, track2);

            Assert.That(Math.Round(trackcoure), Is.EqualTo(Math.Round(225.0)));
        }

        [Test]
        public void CalculateCoursePositiveXandNegativY()
        {
            track1 = new TrackObject("Tag123", 80000, 60000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            track2 = new TrackObject("Tag123", 81500, 58000, 1000, DateTime.ParseExact("20180412111322111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            trackcoure = _uut.CalculateCompassCourse(track1, track2);

            Assert.That(Math.Round(trackcoure), Is.EqualTo(Math.Round(143.0)));
        }

        [Test]

        public void CalculateCourseNegativeXandPostiveY()
        {
            track1 = new TrackObject("Tag123", 80000, 60000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            track2 = new TrackObject("Tag123", 70500, 62000, 1000, DateTime.ParseExact("20180412111322111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            trackcoure = _uut.CalculateCompassCourse(track1, track2);

            Assert.That(Math.Round(trackcoure), Is.EqualTo(Math.Round(282.0)));
        }




    }
}
