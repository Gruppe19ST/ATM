using System;
using System.Collections.Generic;
using System.Globalization;
using ATM.Logic.Controllers;
using ATM.Logic.Handlers;
using ATM.Logic.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    /*
     * This Unit test class does not contain real, automatic unit tests, as the controller doesn't give an output except the one to the Console.
     * The tests in this class are tests, where the developer can look at the test output and see, that it is as expected - or check the console
     * However the unit test class has been helpful in debugging the actions on the Console
     */
    [TestFixture]
    public class ControllerUnitTest
    {
        private ISorter _sorter;
        private ITrackController _uut;
        private ITrackSpeed _speed;
        private ITrackCompassCourse _compassCourse;
        private ISeperationEventChecker _checker;
        private ISeperationEventHandler _warningCreator;
        private ISeperationEventLogger _logger;

        private TrackObjectEventArgs _receivedArgs;
        private TrackObjectEventArgs _fakeTrackArgs;
        private List<TrackObject> _listOfTracks;
        private TrackObject _track1, _track2, _track3, _track4;

        [SetUp]
        public void SetUp()
        {
            _sorter = Substitute.For<ISorter>();
            _speed = Substitute.For<ITrackSpeed>();
            _compassCourse = Substitute.For<ITrackCompassCourse>();
            _checker = Substitute.For<ISeperationEventChecker>();
            _warningCreator = Substitute.For<ISeperationEventHandler>();
            _logger = Substitute.For<ISeperationEventLogger>();
            _uut = new Controller(_sorter,_speed,_compassCourse,_checker,_warningCreator,_logger);

            _sorter.TrackSortedReady += (o, args) =>
            {
                _receivedArgs = args;
            };

            _listOfTracks = new List<TrackObject>();

            // Create tracks
            _track1 = new TrackObject("Tag123", 70000, 70000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 68000, 68000, 800, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track3 = new TrackObject("Tag789", 89000, 89000, 5000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track4 = new TrackObject("TagABC", 72000, 72000, 1200, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
        }

        [Test]
        public void noPriorTracks_newListOfCurrentTracks_ShowCurrentTracks()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _fakeTrackArgs = new TrackObjectEventArgs(_listOfTracks);

            _sorter.TrackSortedReady += Raise.EventWith(_fakeTrackArgs);
            //_uut.HandleTrack();
        }

        [Test]
        public void somePriorTracks_newListOfCurrentTracks_ShowCurrentTracks()
        {
            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _fakeTrackArgs = new TrackObjectEventArgs(_listOfTracks);

            _sorter.TrackSortedReady += Raise.EventWith(_fakeTrackArgs);

            _listOfTracks.Clear();
            _track1 = new TrackObject("Tag123", 72000, 72000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _fakeTrackArgs = new TrackObjectEventArgs(_listOfTracks);

            _sorter.TrackSortedReady += Raise.EventWith(_fakeTrackArgs);

        }
    }
}