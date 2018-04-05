using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class AtmUnitTest
    {
        private TrackConverter _uut;
        private ITransponderReceiver _transponderReceiver;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TrackConverter(_transponderReceiver);
        }

        //[Test]

    }
}
