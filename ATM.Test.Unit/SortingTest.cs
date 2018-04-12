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
            private List<TrackObject> _tracks;

            [SetUp]

            public void SetUp()
            {
                _uut = new Logic.Handlers.Sorter();
                _tracks = new List<TrackObject>();

            }

            [Test]
        
            public void Sorter_Lav_Y()
            {
                _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 15000, YCoordinate = 10000 });
                _tracks.Add(new TrackObject() { Altitude = 5000, XCoordinate = 15000, YCoordinate = 11000 });

                _uut.SortTracks(_tracks);
                Assert.That(_uut.SortedList.Count.Equals(1));
            }

            public void Sorter_Høj_Y()
            {

            }

            public void Sorter_Lav_X()
            {

            }

            public void Sorter_Høj_X()
            {

            }

            public void Sorter_Lav_Alt()
            {

            }

            public void Sorter_Høj_Alt()
            {

            }

        }
    }

