using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using NSubstitute;
using ATM;
using ATM.Logic.Handlers;

namespace ATM.Test.Unit
{
    [TestFixture]
    class SortingTest
    {
        private Logic.Handlers.Sorter _uut;
        private Logic.Interfaces.ITrackConverter trackconverter; 

        private List<TrackObject> _tracks;
        private TrackObject trackobject;

        private int _nEventsRaised;
        private TrackObjectEventArgs _receivedArgs;

        [SetUp]
        public void SetUp()
        {
            trackconverter = Substitute.For<Logic.Interfaces.ITrackConverter>();
            _uut = new Logic.Handlers.Sorter(trackconverter);
            _tracks = new List<TrackObject>();
            trackobject = new TrackObject("123", 12345, 12345, 1000, DateTime.ParseExact("20151006213456789", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _tracks.Add(trackobject);

            _nEventsRaised = 0;

            _uut.TrackSortedReady += (o, args) =>
            {
                ++_nEventsRaised;
                _receivedArgs = args;
            };
        }


        [Test]
            public void RaiseEvent()
        {
            var args = new TrackObjectEventArgs(new List<TrackObject> {trackobject});

            trackconverter.TrackObjectsReady += Raise.EventWith(args);
            Assert.That(_nEventsRaised, Is.EqualTo(1));
        }

        [Test]
        public void Sorter_Lav_Y()
        {
            _tracks.Clear();
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 15000, YCoordinate = 10000 });
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 15000, YCoordinate = 11000 });

            _uut.SortTracks(_tracks);
            Assert.That(_uut.SortedList.Count, Is.EqualTo(1));
        }

        [Test]
        public void Sorter_Høj_Y()
        {
            _tracks.Clear();
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 15000, YCoordinate = 90000 });
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 15000, YCoordinate = 89000 });

            _uut.SortTracks(_tracks);
            Assert.That(_uut.SortedList.Count, Is.EqualTo(1));
        }

        [Test]
        public void Sorter_Lav_X()
        {
            _tracks.Clear();
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 10000, YCoordinate = 15000 });
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 11000, YCoordinate = 15000 });

            _uut.SortTracks(_tracks);
            Assert.That(_uut.SortedList.Count, Is.EqualTo(1));
        }

        [Test]
        public void Sorter_Høj_X()
        {
            _tracks.Clear();
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 90000, YCoordinate = 15000 });
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 89000, YCoordinate = 15000 });

            _uut.SortTracks(_tracks);
            Assert.That(_uut.SortedList.Count, Is.EqualTo(1));
        }

        [Test]
        public void Sorter_Lav_Alt()
        {
            _tracks.Clear();
            _tracks.Add(new TrackObject() { Altitude = 500, XCoordinate = 15000, YCoordinate = 15000 });
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 15000, YCoordinate = 15000 });

            _uut.SortTracks(_tracks);
            Assert.That(_uut.SortedList.Count, Is.EqualTo(1));
        }

        [Test]
        public void Sorter_Høj_Alt()
        {
            _tracks.Clear();
            _tracks.Add(new TrackObject() { Altitude = 20000, XCoordinate = 10000, YCoordinate = 15000 });
            _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 11000, YCoordinate = 15000 });

            _uut.SortTracks(_tracks);
            Assert.That(_uut.SortedList.Count, Is.EqualTo(1));
        }

    }
}

