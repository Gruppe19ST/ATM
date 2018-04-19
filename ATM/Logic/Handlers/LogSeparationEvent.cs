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
        /*// Filename
        static string filename = "E:/Visual Studio 2017/SWT/ATM/test.txt";

        // Create/open stream and file
        FileStream output = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);*/

        public LogSeparationEvent(ISeperationEventChecker checker)
        {
            checker.SeperationEvents += (o, trackArgs) => SaveToFile(trackArgs);
        }
        
        public void SaveToFile(SeparationEventArgs separationEvent)
        {
            // Input to file
            string input = $"{separationEvent.SeparationObjects[0].Tag}, {separationEvent.SeparationObjects[1].Tag}, {Convert.ToString(separationEvent.SeparationObjects[0].TimeStamp)}";

            using (StreamWriter writer = File.AppendText("E:/Visual Studio 2017/SWT/ATM/test.txt"))
            {
                writer.WriteLine(input);
            }

            /*    try
                {
                    // Filename
                    string filename = "E:/Visual Studio 2017/SWT/ATM/test.txt";

                    // Create/open stream and file
                    FileStream output = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);

                    // Create writing-object that can write to output
                    StreamWriter fileWriter = new StreamWriter(output);

                    // Write til file
                    fileWriter.WriteLine(input);

                    // Close file and stream
                    fileWriter.Close();
                    output.Close();

                }
                catch (IOException e)
                {

                }*/
        }
    }
}
