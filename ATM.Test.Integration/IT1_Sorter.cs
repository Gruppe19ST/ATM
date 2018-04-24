using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using ATM.Logic.Handlers;
using ATM.Logic.Interfaces;
using ATM.Logic.Controllers;


namespace ATM.Test.Integration
{
    [TestFixture]
    public class IT1_Sorter
    {
        //Drivers
        private FakeDLL _fakeDLL;

        //system under test
        private TrackConverter _sut;

        // Interface under test 
        private ISorter _fakeSorter;


        [SetUp]
        public void SetUp()
        {
            _fakeDLL = new FakeDLL();
            _sut = new TrackConverter(_fakeDLL);
            _fakeSorter = Substitute.For<ISorter>();
        }

       
    }
}
