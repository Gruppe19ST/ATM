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
using TransponderReceiver;

namespace ATM.Test.Integration
{
    [TestFixture]
    class IT4_Controller_CheckSeperationEvent
    {
        #region Defining objects
        // Drivers/included
        private ITransponderReceiver _receiver;
        private ITrackConverter _trackConverter;
        private ISorter _sorter;

        // System under test
        private ITrackController _controller;
        private ISeperationEventChecker _checker;

        // Stubs/mocks
        private ISeperationEventLogger _logger;
        private ISeperationEventHandler _warningCreator;
        private ITrackSpeed _ts;
        private ITrackCompassCourse _tcc;

        // Data
        private RawTransponderDataEventArgs _fakeRawArgs;
        private SeparationEventArgs _separationArgs;
        private SeparationEventArgs _finishedSeparationArgs;

        /*
        private List<TrackObject> _listOfTracks;
        private TrackObject _track1, _track2, _track3;*/
        #endregion

        #region Setup
        [SetUp]
        public void Setup()
        {
            // Drivers/included
            _receiver = Substitute.For<ITransponderReceiver>();
            _trackConverter = new TrackConverter(_receiver);
            _sorter = new Sorter(_trackConverter);
            
            // System under test
            _checker = new CheckForSeparationEvent();

            // Stubs/mocks
            _logger = Substitute.For<ISeperationEventLogger>();
            _warningCreator = Substitute.For<ISeperationEventHandler>();
            _ts = Substitute.For<ITrackSpeed>(); 
            _tcc = Substitute.For<ITrackCompassCourse>(); 


            // System under test
            _controller = new Controller(_sorter, _ts, _tcc, _checker, _warningCreator, _logger);
            

            // Data
            _fakeRawArgs = new RawTransponderDataEventArgs(new List<string>()
            {
                "Tag123;70000;70000;1000;20180420222222222","Tag456;68000;68000;800;20180420222222222", "Tag789;89000;89000;5000;20180420222222222"
            });

            // Assign to events
            _checker.SeperationEvents += _checker_SeperationEvents;
            _checker.NewSeperationEvents += _checker_FinishedSeperationEvents;

        }

        /*
         * Save arguments when events appear
         */
        private void _checker_SeperationEvents(object sender, SeparationEventArgs e)
        {
            _separationArgs = e;
        }

        private void _checker_FinishedSeperationEvents(object sender, SeparationEventArgs e)
        {
            _finishedSeparationArgs = e;
        }
        #endregion

        #region Tests

        // Test that an event indicating new separation information is raised
        [Test]
        public void HandleTrack_CheckSeparationEvents_RaiseSeparationEvent()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            Assert.That(_separationArgs.SeparationObjects.Count, Is.EqualTo(1));
        }

        // Test that an event indicating finished separations is raised
        [Test]
        public void HandleTrack_FinishedSeparationEvents_RaiseFinishedEvent()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);

            _fakeRawArgs = new RawTransponderDataEventArgs(new List<string>()
            {
                "Tag123;70000;70000;1000;20180420222222222",
                "Tag456;64000;64000;800;20180420222222222",
                "Tag789;89000;89000;5000;20180420222222222"
            });

            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);

            Assert.That(_finishedSeparationArgs.SeparationObjects.Count ,Is.EqualTo(1));
        }

        #endregion
    
    }
}
