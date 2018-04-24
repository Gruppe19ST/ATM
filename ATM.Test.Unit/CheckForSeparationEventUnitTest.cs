using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic;
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
        private SeparationEventArgs _newArgs;

        [SetUp]
        public void SetUp()
        {
            _listOfTracks = new List<TrackObject>();

            _track1 = new TrackObject("Tag123", 70000,70000,1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 68000, 68000, 800, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track3 = new TrackObject("Tag789", 89000,89000,5000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track4 = new TrackObject("TagABC", 72000, 72000, 1200, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));


            _uut = new CheckForSeparationEvent();
            _uut.SeperationEvents += (o, args) => {
                _receivedArgs = args;
            };
            _uut.NewSeperationEvents += (o, args) => { _newArgs = args; };
        }
        

        [Test]
        public void checkSeparationOf2Objects_1TooClose_1SeparationEvent()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);

            _uut.CheckSeparationEvents(_listOfTracks);

            // Assume, that when 1 pair of tracks is too close, this creates 1 separation event object
            Assert.That(_receivedArgs.SeparationObjects.Count, Is.EqualTo(1));
            
        }

        [Test]
        public void checkSeparationOf3Objects_1TooClose_1SeparationEvent()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track3);

            _uut.CheckSeparationEvents(_listOfTracks);

            // Assume, that when 1 pair of tracks is too close, this creates 1 separation event object
            // and that this happens, eventhough 3 tracks are now in the air
            Assert.That(_receivedArgs.SeparationObjects.Count, Is.EqualTo(1));
        }

        [Test]
        public void checkSeparationOf3Objects_2TooClose_2SeparationEvents()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track4);

            _uut.CheckSeparationEvents(_listOfTracks);

            // Assume, that when 2 pair of tracks are too close, this creates 2 separation events
            Assert.That(_receivedArgs.SeparationObjects.Count, Is.EqualTo(2));
        }

        [Test]
        public void checkSeparationOf4Objects_3TooClose_3SeparationEvents()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(new TrackObject("Tag789", 73000, 73000, 1400, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));
            _listOfTracks.Add(_track4);
            //Track 1+2, 1+3, 3+4 are in conflict

            _uut.CheckSeparationEvents(_listOfTracks);

            // Assume, that when 3 pair of tracks are too close, this creates 3 separation events
            Assert.That(_receivedArgs.SeparationObjects.Count, Is.EqualTo(3));
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

            _uut.CheckSeparationEvents(_listOfTracks);

            Assert.That(_receivedArgs, Is.EqualTo(null));
        }
        

        [Test]
        public void checkSeparation_NewSeparation_EventRaised()
        {
            // Create a separationevent
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);

            _uut.CheckSeparationEvents(_listOfTracks);

            // Create a new separationevent
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track3);
            _listOfTracks.Add(new TrackObject("TagABC", 87500, 88000, 4800, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));

            _uut.CheckSeparationEvents(_listOfTracks);
            // Check that the event was raised with the right info
            Assert.That(_newArgs.SeparationObject.Tag1, Is.EqualTo("Tag789"));
        }
        
        /*
        [Test]
        public void checkSeparationOf3Objects_2TooClose_NoFinishedEvents()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track4);

            _uut.CheckSeparationEvents(_listOfTracks);

            // Assume that no finished events are there yet
            Assert.That(_newArgs, Is.EqualTo(null));
        }
        */
    }

}
