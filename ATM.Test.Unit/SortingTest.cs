using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using NSubstitute;
using ATM;

namespace ATM.Test.Unit
{
        [TestFixture]
    class SortingTest
    {
        private ATM.Logic.Handlers.Sorter _uut;
        private ATM.Logic.Interfaces.ITrackReceiver trackreceiver; 

        private List<TrackObject> _tracks;
        private TrackObject trackobject;

        private int _nEventsRaised;
        private TrackObjectEventArgs _receivedArgs;

        [SetUp]

        public void SetUp()
        {
            trackreceiver = Substitute.For<Logic.Interfaces.ITrackReceiver>();
            _uut = new Logic.Handlers.Sorter(trackreceiver);
            _tracks = new List<TrackObject>();
            trackobject = new TrackObject("123", 12345, 12345, 1000, Convert.ToDateTime(20151006213456789));
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
            var args = new Raw;
        }

            //public void Sorter_Lav_Y()
            //{
            //    _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 15000, YCoordinate = 10000 });
            //    _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 15000, YCoordinate = 11000 });

            //    _uut.SortTracks(_tracks);
            //    Assert.That(_uut.SortedList.Count.Equals(1));
            //}

            //public void Sorter_Høj_Y()
            //{

            //}

            //public void Sorter_Lav_X()
            //{

            //}

            //public void Sorter_Høj_X()
            //{

            //}

            //public void Sorter_Lav_Alt()
            //{

            //}

            //public void Sorter_Høj_Alt()
            //{

            //}

        }
    }

