using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;
using ATM.Logic.Handlers;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    class TrackSpeedUnitTest
    {
        private TrackObject _trackPrior, _trackCurrent;
        private TrackSpeed _uut;
        private double horizontalVelocity;

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackSpeed();
            horizontalVelocity = 0;
        }

        [Test]
        public void calculateSpeed_PositiveXandY_HorizontalVelocityIsCorrect()
        {
            _trackPrior = new TrackObject("Tag123", 80000, 60000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _trackCurrent = new TrackObject("Tag123", 80500, 60400, 1000, DateTime.ParseExact("20180412111322111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            horizontalVelocity = _uut.CalculateSpeed(_trackCurrent, _trackPrior);
            Assert.That(Math.Round(horizontalVelocity), Is.EqualTo(Math.Round(4.8879)));
        }
        [Test]
        public void calculateSpeed_NegativeXandY_HorizontalVelocityIsCorrect()
        {
            _trackPrior = new TrackObject("Tag123", 80000, 60000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _trackCurrent = new TrackObject("Tag123", 70500, 50400, 1000, DateTime.ParseExact("20180412111322111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            horizontalVelocity = _uut.CalculateSpeed(_trackCurrent, _trackPrior);
            Assert.That(Math.Round(horizontalVelocity), Is.EqualTo(Math.Round(103.0987)));
        }
        [Test]
        public void calculateSpeed_NegativeXandPositiveY_HorizontalVelocityIsCorrect()
        {
            _trackPrior = new TrackObject("Tag123", 80000, 60000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _trackCurrent = new TrackObject("Tag123", 70500, 62000, 1000, DateTime.ParseExact("20180412111322111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            horizontalVelocity = _uut.CalculateSpeed(_trackCurrent, _trackPrior);
            Assert.That(Math.Round(horizontalVelocity), Is.EqualTo(Math.Round(74.1087))); 
        }
        [Test]
        public void calculateSpeed_PositiveXandNegativeY_HorizontalVelocityIsCorrect()
        {
            _trackPrior = new TrackObject("Tag123", 80000, 58000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _trackCurrent = new TrackObject("Tag123", 81500, 56000, 1000, DateTime.ParseExact("20180412111322111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            horizontalVelocity = _uut.CalculateSpeed(_trackCurrent, _trackPrior);
            Assert.That(Math.Round(horizontalVelocity), Is.EqualTo(Math.Round(19.0840)));
        }



    }
}
