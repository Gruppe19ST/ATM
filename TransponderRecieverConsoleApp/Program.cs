using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using ATM;
using ATM.Logic.Handlers;
using ATM.Logic.Handlers.Converter;

namespace TransponderRecieverConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            TrackConverter trackConverter = new TrackConverter(TransponderReceiverFactory.CreateTransponderDataReceiver());
            //trackConverter.TrackObjectsReady += TrackConverter_TrackObjectsReady;*/
            TrackReceiver trackReceiver = new TrackReceiver(trackConverter);
            

            Console.ReadLine(); 
            
        }

        private static void TrackConverter_TrackObjectsReady(object sender, TrackObjectEventArgs e)
        {
            foreach (var track in e.TrackObjects)
            {
                Console.WriteLine(track.ToString());
            }
        }
    }
}
