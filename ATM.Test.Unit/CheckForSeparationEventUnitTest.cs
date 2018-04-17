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
        private TrackObject _track1, _track2, _track3; // _track4;
       // List<List<TrackObject>> _conflictedTracksList;

        private CheckForSeparationEvent _uut;
        private SeparationEventArgs _receivedArgs;

        [SetUp]
        public void SetUp()
        {
            _listOfTracks = new List<TrackObject>();

            _track1 = new TrackObject("Tag123", 70000,70000,1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 68000, 68000, 800, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track3 = new TrackObject("Tag789", 72000, 72000, 1200, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            //_track4 = new TrackObject("TagABC", 89000,89000,5000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
        }

        public void SeparationEvent()
        {
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track3);

            _uut = new CheckForSeparationEvent(_listOfTracks);
            _uut.CheckSeparationEvents();
            _uut.SeperationEvents += (o, args) => {
                _receivedArgs = args;

            };
        }

        [Test]
        public void checkSeparation_TooClose_ListContains2()
        {
            SeparationEvent();

           Assert.That(_receivedArgs, Is.EqualTo(1));
        }

        [Test]
        public void checkSeparation_NotTooClose_ReturnEmptyList()
        {
            _track2.XCoordinate = 10000;
            _track2.YCoordinate = 10000;
            _track2.Altitude = 5000;

            //Assert.That(_conflictedTracksList, Is.EqualTo(null));
        }
    }
}
