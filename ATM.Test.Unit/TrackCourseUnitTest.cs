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
    class TrackCourseUnitTest
    {
        private ATM.Logic.Handlers.TrackCompassCourse _uut;

        
        private TrackObject track1;
        private TrackObject track2;

        [SetUp]

        public void Setup()
        {
            _uut = new ATM.Logic.Handlers.TrackCompassCourse();
        }

        [TestCase("Hej", 10000, 15000, 10000, 15000, 315)]
        [TestCase("Hej", 10000, 5000, 10000, 15000, 45)]
        [TestCase("Hej", 10000, 5000, 10000, 5000, 135)]
        [TestCase("Hej", 1000, 15000, 10000, 5000, 225)]
        public void CourseTest()
        {
            _uut.CalculateCompassCourse(track1, track2);
        }

    }
}
