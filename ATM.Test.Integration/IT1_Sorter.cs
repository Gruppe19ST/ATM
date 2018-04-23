using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class IT1_Sorter
    {
        //Drivers
        private ITransponderReceiver _receiver;
        private RawTransponderDataEventArgs _fakeRawArgs;
        
        //system under test
        private TrackConverter _converter;
        private Sorter _sorter;

        // Data
        private List<TrackObject> _sortedTrackList;
        private TrackObjectEventArgs _trackObjectEvent;
        
        [SetUp]
        public void SetUp()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _converter = new TrackConverter(_receiver);
            _sorter = new Sorter(_converter);
            _sorter.TrackSortedReady += (o, args) => _sortedTrackList = args.TrackObjects;
            _fakeRawArgs = new RawTransponderDataEventArgs(new List<string>()
            {
                "Fly1;88000;88000;6000;20180420222222222","Fly2;72000;91000;19999;20180420222222222", "Fly3;86000;86000;6500;20180420222222222"
            });
              
        }

        [Test]
        public void EventRaisedInConverter_EventDetectedInSorter()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            Assert.That(_sortedTrackList.Count,Is.EqualTo(2));
        }

    }
}
