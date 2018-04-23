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
        private TrackConverter _sut;

        // Interface under test 
        private ISorter _iut;

        // Data
        private List<TrackObject> _trackObjectList;
        private List<TrackObject> _convertedList;
        private TrackObject _t1, _t2, _t3;




        [SetUp]
        public void SetUp()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _sut = new TrackConverter(_receiver);
            
            _fakeRawArgs = new RawTransponderDataEventArgs(new List<string>()
            {
                "Fly1;88000;88000;6000;20180420222222222","Fly2;72000;91000;19999;20180420222222222", "Fly3;86000;86000;6500;20180420222222222"
            });
            _sut.TrackObjectsReady += (o,args) => _trackObjectList = args.TrackObjects;
            _iut = Substitute.For<ISorter>();
            
            _convertedList=new List<TrackObject>();
            
            _t1 = new TrackObject("Fly1", 88000, 88000, 6000, DateTime.ParseExact("20180420222222222", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _t2 = new TrackObject("Fly2", 72000, 91000, 19999, DateTime.ParseExact("20180420222222222", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _t3 = new TrackObject("Fly3", 86000, 86000, 6500, DateTime.ParseExact("20180420222222222", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));

            _convertedList.Add(_t1);
            _convertedList.Add(_t2);
            _convertedList.Add(_t3);

        }

        [Test]
        public void EventRaisedInConverter_EventDetectedInSorter()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeRawArgs);
            
            //_iut.Received().SortTracks(_convertedList);
            // Hvordan i alverden får jeg min fake-sorteringsklasse til at registrere det event der raises i controlleren? 
            // Det er højst sandsynligt muligt at raise et event i converteren med allerede konverterede tracks, 
            // men så er det jo ikke transponderrecieveren der er driveren? 
        }

    }
}
