using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
{
    public class LogSeparationEvent : ISeperationEventLogger
    {
        // Filename
        private static readonly string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
        static string fileName = "test.txt";
        private static readonly string FilePath = System.IO.Path.Combine(Path, fileName);

        /*
        // Create/open stream and file
        FileStream output = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);*/

        public LogSeparationEvent(ISeperationEventChecker checker)
        {
            checker.SeperationEvents += (o, trackArgs) => SaveToFile(trackArgs);
        }
        
        public void SaveToFile(SeparationEventArgs separationEvent)
        {
            // Input to file
            string input = $"{Convert.ToString(separationEvent.SeparationObjects[0].TimeStamp)}, {separationEvent.SeparationObjects[0].Tag}, {separationEvent.SeparationObjects[1].Tag}";

            using (StreamWriter writer = File.AppendText(FilePath))
            {
                writer.WriteLine(input);
            }
        }
    }
}
