using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using ATM;
using ATM.Logic.Handlers;
using ATM.Logic.Controllers;
using ATM.Logic.Interfaces;


namespace TransponderRecieverConsoleApp
{
    class Program
    {
        // private static List<TrackObject> _listOfTracks;
       // private static TrackObject _track1, _track2, _track3;

        static void Main(string[] args)
        {

            TrackConverter trackConverter = new TrackConverter(TransponderReceiverFactory.CreateTransponderDataReceiver());
            Sorter sorter = new Sorter(trackConverter);
            TrackSpeed ts = new TrackSpeed();
            TrackCompassCourse tcc = new TrackCompassCourse();
            CheckForSeparationEvent checker = new CheckForSeparationEvent();
            CreateWarning warner = new CreateWarning(checker);
            LogSeparationEvent logger = new LogSeparationEvent(checker);

            Controller controller = new Controller(sorter,ts,tcc,checker,warner,logger);
            Console.ReadLine(); 
            
        }

        
    }
}
