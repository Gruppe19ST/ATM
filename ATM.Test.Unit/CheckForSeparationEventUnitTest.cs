using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ATM.Logic.Handlers;
using ATM.Logic.Interfaces;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class CheckForSeparationEventUnitTest
    {
        
        private List<TrackObject> _listOfTracks;
        private TrackObject _track1, _track2;

        private ISorter _sorter;
        private ITrackController _controller;
        private CheckForSeparationEvent _uut;

        [SetUp]
        public void SetUp()
        {
            _sorter = Substitute.For<ISorter>();
            _controller = Substitute.For<ITrackController>();

            _track1 = new TrackObject("Tag123", 70000,70000,1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 70000, 70000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _uut = new CheckForSeparationEvent(_listOfTracks);
        }

        [Test]
        public void checkSeparation_TooClose_ReturnList()
        {
            _track2.XCoordinate = 66000;
            _track2.YCoordinate = 66000;
            _track2.Altitude = 1100;

            Assert.That(_uut.CheckSeparationEvents().Count, Is.GreaterThan(0));
        }
    }
}
