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

        /*
         * Filepath etc. for the location of the logfile
         */
        private static readonly string Path = System.Environment.CurrentDirectory;
        static string fileName = "test.txt";
        private static readonly string FilePath = System.IO.Path.Combine(Path, fileName);

        [SetUp]
        public void SetUp()
        {
            _checker = Substitute.For<ISeperationEventChecker>();
            _uut = new LogSeparationEvent(_checker);

            // Clearing variable for eventhandling
            _receivedArgs = null;

            // Assigning to event and saving the args in variable
            _checker.NewSeperationEvents += (o, args) => { _receivedArgs = args; };
        }

        #region Test

        // Test to see that a new separation event is saved in the log file
        [Test]
        public void OneNewSeparationEvent_ReceiveEvent_LogEvent()
        {
            // Create event args
            var separationEvent = new SeparationEventArgs(new SeparationEventObject("Tag123",
                "Tag456",
                DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));

            // Raise with event
            _checker.NewSeperationEvents += Raise.EventWith(separationEvent);

            string lastLine;
            // Open file
            using (File.OpenRead(FilePath))
            {
                // Read the last line from file into variable
                lastLine = File.ReadLines(FilePath).Last();
            }

            // Checking that the newest added line matches the expected
            Assert.That(lastLine, Is.EqualTo("Separation occured at 12-04-2018 11:11:11 between tracks: Tag123, Tag456"));
        }

        // Test to see that a new separation event is saved in the log file as the last line when one separation is already logged
        [Test]
        public void OneMoreNewSeparationEvent_ReceiveEvent_LogEvent()
        {
            // Create 1 separation args and raise event with it
            var separationEvent = new SeparationEventArgs(new SeparationEventObject("Tag123",
                "Tag456",
                DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));
            _checker.NewSeperationEvents += Raise.EventWith(separationEvent);

            // Create a second separation args and raise event with it
            separationEvent = new SeparationEventArgs(new SeparationEventObject("Tag789",
                "TagABC",
                DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));
            _checker.NewSeperationEvents += Raise.EventWith(separationEvent);

            // Open file
            using (File.OpenRead(FilePath))
            {
                // Read the last file into variable
                var lastLine = File.ReadLines(FilePath).Last();

                // Check that the last line matches the newest logged sep. args.
                Assert.That(lastLine,
                    Is.EqualTo("Separation occured at 12-04-2018 11:11:11 between tracks: Tag789, TagABC"));
            }
        }

        // Test to see that a new separation event is saved in the log file as a new line when one separation is already logged
        [Test]
        public void OneMoreNewSeparationEvent_LogEvent_LogOnCorrectPlace()
        {
            // Variables to contain the number of lines before and after logging new separation
            int initialLines;
            int endingLines;

            // Open stream
            using (File.OpenRead(FilePath))
            {
                // Count number of lines and save
                initialLines = File.ReadLines(FilePath).Count();
            }

            // Create event args and raise event with it
            var separationEvent = new SeparationEventArgs(new SeparationEventObject("Tag123",
                "Tag456",
                DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)));
            _checker.NewSeperationEvents += Raise.EventWith(separationEvent);

            // Open straeam
            using (File.OpenRead(FilePath))
            {
                // Count number of lines and save
                endingLines = File.ReadLines(FilePath).Count();
            }

            // Check that the number of lines is increased by one
            Assert.That(endingLines, Is.EqualTo(initialLines+1));
        }
        #endregion
    }
}
