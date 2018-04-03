using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace TransponderRecieverConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var myReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            myReceiver.TransponderDataReady += MyReceiver_TransponderDataReady;

            Console.ReadLine();

        }

        private static void MyReceiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            var receiverData = e.TransponderData;
            PrintData(receiverData);
        }

        private static void PrintData(List<string> data)
        {
            foreach (var d in data)
            {
                Console.WriteLine(d);
            }
        }

        
    }
}
