using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using ATM.Logic.Handlers;
using ATM.Logic.Interfaces;
using ATM.Logic.Controllers;
using TransponderReceiver;

namespace ATM.Test.Integration
{
    class IT3_TrackCourse_TrackSpeed
    {
        //Drivers
        private ITransponderReceiver _receiver;
        private RawTransponderDataEventArgs _fakeRawArgs;
        private RawTransponderDataEventArgs _fakeRawArgs2;

        //system under test
        private Controller _controller;
        private TrackSpeed _ts;
        private TrackCompassCourse _tcc;

        // Included 
        private TrackConverter _converter;
        private Sorter _sorter;

        // Data
        private List<TrackObject> _priorList;
        
        // Fakes
        private ISeperationEventChecker _checker;
        private ISeperationEventHandler _warningCreator;
        private ISeperationEventLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _converter = new TrackConverter(_receiver);
            _sorter = new Sorter(_converter);
            _ts = new TrackSpeed();
            _tcc = new TrackCompassCourse();
            _checker = Substitute.For<ISeperationEventChecker>();
            _warningCreator = Substitute.For<ISeperationEventHandler>();
            _logger = Substitute.For<ISeperationEventLogger>();

            _controller = new Controller(_sorter, _ts, _tcc, _checker, _warningCreator, _logger);

            _fakeRawArgs = new RawTransponderDataEventArgs(new List<string>()
            {
                "Fly1;88000;88000;6000;20180420222222222","Fly2;72000;91000;19999;20180420222222222", "Fly3;86000;86000;6500;20180420222222222"
            });
            _fakeRawArgs2 = new RawTransponderDataEventArgs(new List<string>()
            {
                "Fly1;86000;86000;6000;20180420223222222","Fly2;72000;91000;19999;20180420223222222", "Fly3;86000;86000;6500;20180420223222222"
            });

        }

        [Test]
        public void CalculateSpeed_CorrectTrackSpeedReturnedToController()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs2);
            Assert.That(_controller.priorTracks[0].horizontalVelocity, Is.EqualTo(4.71));
        }

        [Test]
        public void CalculateCourse_CorrectTrackCourseReturnedToController()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs2);
            Assert.That(_controller.priorTracks[0].compassCourse, Is.EqualTo(45));
        }

    }
}
