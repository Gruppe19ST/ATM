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
using ATM.Logic.Interfaces;

namespace TransponderRecieverConsoleApp
{
    class Program
    {
        private static List<TrackObject> _listOfTracks;
        private static TrackObject _track1, _track2, _track3;

        static void Main(string[] args)
        {
            TrackConverter trackConverter = new TrackConverter(TransponderReceiverFactory.CreateTransponderDataReceiver());
            //trackConverter.TrackObjectsReady += TrackConverter_TrackObjectsReady;*/
            //TrackReceiver trackReceiver = new TrackReceiver(trackConverter);
            Sorter _sorter = new Sorter(trackConverter);

            _listOfTracks = new List<TrackObject>();
            _track1 = new TrackObject("Tag123", 70000, 70000, 1000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track2 = new TrackObject("Tag456", 68000, 68000, 800, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            _track3 = new TrackObject("Tag789", 72000, 72000, 1200, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            //_track4 = new TrackObject("TagABC", 89000,89000,5000, DateTime.ParseExact("20180412111111111", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));

            _listOfTracks.Clear();
            _listOfTracks.Add(_track1);
            _listOfTracks.Add(_track2);
            _listOfTracks.Add(_track3);

            CheckForSeparationEvent separationChecker = new CheckForSeparationEvent(_listOfTracks);
            LogSeparationEvent separationLogger = new LogSeparationEvent(separationChecker);
            separationChecker.CheckSeparationEvents();

            

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
