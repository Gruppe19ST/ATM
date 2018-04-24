using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
{
    public class LogSeparationEvent : ISeperationEventLogger
    {
        // Filename
        private static readonly string Path = System.Environment.CurrentDirectory;
        static string fileName = "test.txt";
        private static readonly string FilePath = System.IO.Path.Combine(Path, fileName);

        public LogSeparationEvent(ISeperationEventChecker checker)
        {
            checker.NewSeperationEvents += Checker_FinishedSeperationEvents;
        }

        public void Checker_FinishedSeperationEvents(object sender, SeparationEventArgs e)
        {
            foreach (var separationObject in e.SeparationObjects)
            {
                // Input to file
                string input = $"Separation from {Convert.ToString(separationObject.FirstTime, Thread.CurrentThread.CurrentCulture)} to {Convert.ToString(separationObject.FirstTime, Thread.CurrentThread.CurrentCulture)} with tracks: " +
                               $"{separationObject.Tag1}, {separationObject.Tag2}";

                StreamWriter writer;

                using (writer = File.AppendText(FilePath))
                {
                    writer.WriteLine(input);
                }

            }
        }

    }
}
