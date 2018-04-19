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

        private SeparationEventArgs args;

        [SetUp]
        public void SetUp()
        {
            _checker = Substitute.For<ISeperationEventChecker>();
            _uut = new CreateSeparationEventWarning(_checker);

            _listOfTracks = new List<TrackObject>();
            _conflictedTracks = new List<List<TrackObject>>();

            _track1 = new TrackObject("Tag123", 70000, 70000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 66000, 66000, 1100, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);

            args = new SeparationEventArgs(_listOfTracks);
            _checker.SeperationEvents += Raise.EventWith(args);

        }

        [Test]
        public void createWarning_SeperationEvent_returnWarning()
        {
            Assert.That(_uut.CreateWarning(args), Is.EqualTo(_track1.Tag + " and " + _track2.Tag + " are in conflict"));
        }
        
    }

}
