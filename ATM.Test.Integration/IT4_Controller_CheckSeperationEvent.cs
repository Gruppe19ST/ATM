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
        private CheckForSeparationEvent _checker;

        // Stubs/mocks
        private ISeperationEventLogger _logger;
        private ISeperationEventHandler _warningCreator;

        // Data
        private List<TrackObject> _listOfTracks;
        private TrackObject _track1, _track2, _track3, _track4;

        #endregion

        [SetUp]
        public void Setup()
        {
            // Drivers/included
            _trackConverter = Substitute.For<ITrackConverter>();
            _sorter = Substitute.For<ISorter>();

            // Stubs/mocks
            _logger = Substitute.For<ISeperationEventLogger>();
            _warningCreator = Substitute.For<ISeperationEventHandler>();

            // System under test
            _controller = new Controller(_sorter);
            _checker = new CheckForSeparationEvent();

            _listOfTracks = new List<TrackObject>();

            _track1 = new TrackObject("Tag123", 70000, 70000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 68000, 68000, 800, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track3 = new TrackObject("Tag789", 89000, 89000, 5000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track4 = new TrackObject("TagABC", 9000, 72000, 1200, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track3);
            _listOfTracks.Add(_track4);
        }


        #region CreateWarning

        [Test]
        public void HandleTrack_CheckSeparationEvents_CreateWarningReceiveEvent()
        {
            _sorter.SortTracks(_listOfTracks);
            _controller.HandleTrack();
        }

        #endregion
    }
}
