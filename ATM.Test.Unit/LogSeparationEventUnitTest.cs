using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic;
using ATM.Logic.Handlers;
using ATM.Logic.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class LogSeparationEventUnitTest
    {
        private ISeperationEventChecker _checker;
        private ISeperationEventLogger _uut;
        private SeparationEventArgs _receivedArgs;

        private static readonly string Path = System.Environment.CurrentDirectory;
        static string fileName = "test.txt";
        private static readonly string FilePath = System.IO.Path.Combine(Path, fileName);
        StreamReader _reader;

        [SetUp]
        public void SetUp()
        {
            _checker = Substitute.For<ISeperationEventChecker>();
            _uut = new LogSeparationEvent(_checker);

            _checker.NewSeperationEvents += (o, args) => { _receivedArgs = args; };

        }


        [Test]
        public void OneNewSeparationEvent_ReceiveEvent_LogEvent()
        {
            var separationEvent = new SeparationEventArgs(new SeparationEventObject("Tag123",
                "Tag456",
                DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));
            _checker.NewSeperationEvents += Raise.EventWith(separationEvent);


            var lastLine = File.ReadLines(FilePath).Last();

            //Separation occured at { Convert.ToString(separationObject.TimeOfOcccurence, Thread.CurrentThread.CurrentCulture)}
            //between tracks: { separationObject.Tag1}, { separationObject.Tag2}

            Assert.That(lastLine, Is.EqualTo("Separation occured at 12-04-2018 11:11:11 between tracks: Tag123, Tag456"));

        }
    }
}
