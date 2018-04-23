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
using TransponderReceiver;

namespace ATM.Test.Integration
{
    [TestFixture]
    class IT2_Controller
    {
        //Drivers
        private ITransponderReceiver _receiver;
        private RawTransponderDataEventArgs _fakeRawArgs;

        //system under test
        private Sorter _sut;

        // Interface under test 
        private ITrackController _iut;

        // Included 
        private TrackConverter _trackConverter;

        // Data
        private List<TrackObject> _trackObjectList;

        [SetUp]
        public void SetUp()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _trackConverter = new TrackConverter(_receiver);
            _fakeRawArgs = new RawTransponderDataEventArgs(new List<string>()
            {
                "Fly1;88000;88000;6000;20180420222222222","Fly2;72000;91000;19999;20180420222222222", "Fly3;86000;86000;6500;20180420222222222"
            });

            
            
        }

        [Test]
        public void EventRaisedInSorter_EventDetectedInController()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);           
            _sut = new Sorter(_trackConverter); // listen med trackobjects i converteren har et count på 3, men der bliver aldrig lavet et kald til sortList. 
            _iut = Substitute.For<ITrackController>();




        }

    }
}
