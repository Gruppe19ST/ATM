using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Controllers;
using ATM.Logic.Handlers;
using ATM.Logic.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using NUnit.Framework.Api;

namespace ATM.Test.Integration
{
    [TestFixture]
    class IT4_Controller_CheckSeperationEvent
    {
        #region Defining objects
        // Driver/included
        private ITrackConverter _trackConverter;
        private ISorter _sorter;

        // System under test
        private Controller _controller;
        private ISeperationEventChecker _checker;

        // Stubs/mocks
        private ISeperationEventLogger _logger;
        private ISeperationEventHandler _warningCreator;
        private ITrackSpeed _ts;
        private ITrackCompassCourse _tcc;
        

        // Data
        private List<TrackObject> _listOfTracks;
        private TrackObject _track1, _track2, _track3;
        private SeparationEventArgs _separationArgs;
        private object _objectSender;

        #endregion

        [SetUp]
        public void Setup()
        {
            // Drivers/included
            _trackConverter = Substitute.For<ITrackConverter>();
            _sorter = new Sorter(_trackConverter);


            // System under test
            _checker = new CheckForSeparationEvent();

            // Stubs/mocks
            _logger = Substitute.For<ISeperationEventLogger>();
            _warningCreator = Substitute.For<ISeperationEventHandler>();

            // System under test
            _controller = new Controller(_sorter, _ts, _tcc, _checker, _warningCreator, _logger);
            
            _listOfTracks = new List<TrackObject>();

            _track1 = new TrackObject("Tag123", 70000, 70000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 68000, 68000, 800, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track3 = new TrackObject("Tag789", 89000, 89000, 5000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track3);

            _checker.SeperationEvents += _checker_SeperationEvents;

        }

        private void _checker_SeperationEvents(object sender, SeparationEventArgs e)
        {
            _objectSender = sender;
            _separationArgs = e;
        }


        #region CreateWarning

        [Test]
        public void HandleTrack_CheckSeparationEvents_CreateWarningReceiveEvent()
        {

            // When the method is called, it calls CheckSeparationEvents on the _checker-class
            _controller.CheckTracks(_listOfTracks);
           
            // _checker.CheckSeparationEvents(_listOfTracks);

            // CheckSeparationEvents raises an event with args (=_separationArgs)
            // _warningCreator assigns to this event and handles the information

            Assert.That(_separationArgs.SeparationObjects.Count, Is.EqualTo(1));
        }

        

        #endregion
    }
}
