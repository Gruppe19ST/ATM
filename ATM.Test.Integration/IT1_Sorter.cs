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

        //stubs
        private ITrackController _trackcontroller;

        //included
        private TrackConverter _trackconverter;

        //systemundertest
        private Sorter _sut;

        [SetUp]
        public void SetUp()
        {
            _fakeDLL = new FakeDLL();
            _trackcontroller = Substitute.For<ITrackController>();
            //_trackconverter = new TrackConverter(_fakeDLL);
            _sut = new Sorter(_trackconverter);
        }

       
    }
}
