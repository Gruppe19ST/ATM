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
            TrackConverter trackConverter = new TrackConverter(TransponderReceiverFactory.CreateTransponderDataReceiver());
            Console.ReadLine(); 
            
        }

        

        
    }
}
