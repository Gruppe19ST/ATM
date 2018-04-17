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
    public class CreateSeparationWarningUnitTest
    {
        private List<List<TrackObject>> _conflictedTracks;
        private List<TrackObject> _listOfTracks;
        private TrackObject _track1, _track2;

        private ISeperationEventChecker _checker;
        private CreateSeparationEventWarning _uut;

        [SetUp]
        public void SetUp()
        {
            _checker = Substitute.For<ISeperationEventChecker>();
            _uut = new CreateSeparationEventWarning(_checker);

            _listOfTracks = new List<TrackObject>();
            _conflictedTracks = new List<List<TrackObject>>();

            _track1 = new TrackObject("Tag123", 70000, 70000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 70000, 70000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _conflictedTracks.Add(_listOfTracks);
        }

        [Test]
        public void createWarning_SeperationEvent_returnWarning()
        {
            _track2.XCoordinate = 66000;
            _track2.YCoordinate = 66000;
            _track2.Altitude = 1100;
            
            //Assert.That(_uut.CreateWarning(_conflictedTracks).Count, Is.GreaterThan(0));
        }

        [Test]
        public void createWarning_NoSeparationEvent_noWarning()
        {
            _conflictedTracks.Remove(_listOfTracks);

            //Assert.That(_uut.CreateWarning(_conflictedTracks).Count, Is.EqualTo(0));
        }
    }

}
