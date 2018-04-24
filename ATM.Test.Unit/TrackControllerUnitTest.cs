using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;
using ATM.Logic.Handlers;
using ATM.Logic.Controllers;
using NUnit.Framework.Internal;
using NUnit.Framework;
using NSubstitute;

namespace ATM.Test.Unit
{
    [TestFixture]
    class TrackControllerUnitTest
    {
        private TrackObject _trackPrior, _trackCurrent;
        private Controller _uut;
        private Sorter sorter;
        private ITrackConverter TrackConverter;

        [SetUp]

        public void SetUp()
        {
            TrackConverter = Substitute.For<Logic.Interfaces.ITrackConverter>();
            sorter = new Sorter(TrackConverter);
          //  _uut = new Controller(sorter);
            _trackPrior = new TrackObject("Tag123", 80000, 60000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _trackCurrent = new TrackObject("Tag123", 80500, 60400, 1000, DateTime.ParseExact("20180412111322111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
        }

        [Test]

        public void CheckLists()
        {

        }
    }

}
