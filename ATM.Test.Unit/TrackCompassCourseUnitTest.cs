using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ATM.Logic.Handlers;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class TrackCompassCourseUnitTest
    {
        private TrackCompassCourse _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackCompassCourse();
        }

        [Test]
        public void CalculateCourse_CourseUnder360Degrees()
        {

        }

    }
}
