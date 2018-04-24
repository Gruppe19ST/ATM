using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Controllers;
using ATM.Logic.Handlers;
using ATM.Logic.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.Test.Integration
{
    [TestFixture]
    class IT5_CheckSeparation_Logfile_CreateWarning
    {
        #region Defining objects
        // Drivers/included
        private ITransponderReceiver _receiver;
        private ITrackConverter _trackConverter;
        private ISorter _sorter;
        private ITrackController _controller;
        private ISeperationEventChecker _checker;

        // System under test
        private ISeperationEventLogger _logger;
        private ISeperationEventHandler _warningCreator;

        // Stubs/mocks
        private ITrackSpeed _ts;
        private ITrackCompassCourse _tcc;

        // Data
        private RawTransponderDataEventArgs _fakeRawArgs;
        private SeparationEventArgs _separationArgs;
        private SeparationEventArgs _finishedSeparationArgs;
        #endregion

        #region Setup
        [SetUp]
        public void Setup()
        {
            // Drivers/included
            _receiver = Substitute.For<ITransponderReceiver>();
            _trackConverter = new TrackConverter(_receiver);
            _sorter = new Sorter(_trackConverter);
            _checker = new CheckForSeparationEvent();


            // System under test
            _logger = new LogSeparationEvent(_checker);
            _warningCreator = new CreateWarning(_checker);

            // Stubs/mocks
            _ts = Substitute.For<ITrackSpeed>(); 
            _tcc = Substitute.For<ITrackCompassCourse>(); 

            // Driver
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

        private void _checker_SeperationEvents(object sender, SeparationEventArgs e)
        {
            _separationArgs = e;
        }

        private void _checker_FinishedSeperationEvents(object sender, SeparationEventArgs e)
        {
            _finishedSeparationArgs = e;
        }
        #endregion

        #region Log

        #endregion

    }
}
