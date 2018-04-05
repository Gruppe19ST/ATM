using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using ATM;

namespace TransponderRecieverConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            bool run = true;
            
            var myReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var TOS = new TrackObjectificationSoftware(myReceiver);
            List<TrackObject> tracks = new List<TrackObject>();

            

            while (true)
            {
                tracks = TOS.getTracks();
                PrintObjects(tracks);
                /*if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    run = false;
                }*/
                
            }

            
            

            //myReceiver.TransponderDataReady += MyReceiver_TransponderDataReady;

            Console.ReadLine();

        }

        private static void PrintObjects(List<TrackObject> tracks)
        {
            if (tracks != null)
            {
                foreach (var track in tracks)
                {
                    Console.WriteLine("Tag: " + track.Tag);
                }
            }
        }

        //private static void MyReceiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        //{
        //    var receiverData = e.TransponderData;
        //    PrintData(receiverData);
        //}

        //private static void PrintData(List<string> data)
        //{
        //    foreach (var d in data)
        //    {
        //        Console.WriteLine(d);
        //    }
        //}

        
    }
}
