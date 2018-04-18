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
        private TrackObject _track1, _track2, _track3, _track4;

        private CheckForSeparationEvent _uut;
        private SeparationEventArgs _receivedArgs;
        

        [SetUp]
        public void SetUp()
        {
            _listOfTracks = new List<TrackObject>();

            _track1 = new TrackObject("Tag123", 70000,70000,1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 68000, 68000, 800, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track3 = new TrackObject("Tag789", 89000,89000,5000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track4 = new TrackObject("TagABC", 72000, 72000, 1200, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));

        }

        public void SeparationEvent()
        {
            _uut = new CheckForSeparationEvent(_listOfTracks);
            _uut.SeperationEvents += (o, args) => {
                _receivedArgs = args;
            };
            _uut.CheckSeparationEvents();
        }

        [Test]
        public void checkSeparationOf2Objects_1TooClose_1SeparationEvent()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);

            SeparationEvent();

            Assert.That(_receivedArgs.SeparationObjects.Count, Is.EqualTo(1));
        }

        [Test]
        public void checkSeparationOf3Objects_1TooClose_1SeparationEvent()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track3);

            SeparationEvent();

            Assert.That(_receivedArgs.SeparationObjects.Count, Is.EqualTo(1));
        }

        [Test]
        public void checkSeparationOf3Objects_2TooClose_2SeparationEvents()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track4);

            SeparationEvent();

            Assert.That(_receivedArgs.SeparationObjects.Count, Is.EqualTo(2));
        }

        [Test]
        public void checkSeparation_NotTooClose_ReturnEmptyList()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _track2.XCoordinate = 10000;
            _track2.YCoordinate = 10000;
            _track2.Altitude = 5000;
            _listOfTracks.Add(_track2);
            
            SeparationEvent();

            Assert.That(_receivedArgs, Is.EqualTo(null));
        }
    }
}
