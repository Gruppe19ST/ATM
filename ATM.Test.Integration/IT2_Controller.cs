using System;
using System.Collections.Generic;
using System.Globalization;
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
    [TestFixture]
    class IT2_Controller
    {
        //Drivers
        private ITransponderReceiver _receiver;
        private RawTransponderDataEventArgs _fakeRawArgs;
        private RawTransponderDataEventArgs _fakeRawArgs2;

        //system under test
        private Sorter _sorter;
        private Controller _controller;
        
        // Included 
        private TrackConverter _converter;

        // Data
        private List<TrackObject> _priorList;
        
        // Fakes
        private ISeperationEventChecker _checker;
        private ISeperationEventHandler _warningCreator;
        private ISeperationEventLogger _logger;
        private ITrackSpeed _speed;
        private ITrackCompassCourse _compassCourse;
    


        [SetUp]
        public void SetUp()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _converter = new TrackConverter(_receiver);
            _sorter = new Sorter(_converter);
            _speed = Substitute.For<ITrackSpeed>();
            _compassCourse = Substitute.For<ITrackCompassCourse>();
            _checker = Substitute.For<ISeperationEventChecker>();
            _warningCreator = Substitute.For<ISeperationEventHandler>();
            _logger = Substitute.For<ISeperationEventLogger>();
            
            _controller = new Controller(_sorter,_speed,_compassCourse,_checker,_warningCreator,_logger);
            
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
        public void FirstEventFromSorter_CorrectTracksInPrior_Fly1At88000()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            Assert.That(_controller.priorTracks[0].XCoordinate, Is.EqualTo(88000));
        }

        [Test]
        public void SecondEventFromSorter_CorrectTracksInPrior_Fly1At86000()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs2);
            Assert.That(_controller.priorTracks[0].XCoordinate, Is.EqualTo(86000));
        }
        
        [Test]
        public void handletracks_CorrectCallToSpeed()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs2);
            _speed.ReceivedWithAnyArgs().CalculateSpeed(new TrackObject(), new TrackObject() );
        }
        [Test]
        public void handletracks_CorrectCallToCompassCourse()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs2);
            _compassCourse.ReceivedWithAnyArgs().CalculateCompassCourse(new TrackObject(), new TrackObject());
        }

        [Test]
        public void checkTracks_CorrectCallToCheckForSeperation()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            _checker.ReceivedWithAnyArgs().CheckSeparationEvents(new List<TrackObject>());
        }


    }
}
