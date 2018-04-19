using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Handlers;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Controllers
{
    public class Controller : ITrackController
    {
        List<TrackObject> priorTracks;
        List<TrackObject> currentTracks;
        private ISorter _sorter;
        private ISeperationEventChecker _checker;
        private ISeperationEventHandler _warningCreator;
        private TrackSpeed ts;
        private TrackCompassCourse tcc;

        //public Controller(ISorter sorter, ISeperationEventChecker checker, ISeperationEventHandler warningCreator)
        public Controller(ISorter sorter)
        {
            currentTracks = new List<TrackObject>();
            _sorter = sorter;
            //_checker = checker;
            //_warningCreator = warningCreator;

            _sorter.TrackSortedReady += _sorter_TrackSortedReady;
            //_checker.SeperationEvents += _checker_SeperationEvents;


            ts = new TrackSpeed();
            tcc = new TrackCompassCourse();
        }

        

        private void _sorter_TrackSortedReady(object sender, TrackObjectEventArgs e)
        { 
            // Nu virker denne metode, DOG overskriver vi jo currentTracks, så hastigheden er jo sat til 0 næste gang der kommer en liste. 
            // Alternativt skal vi prøve om vi kan opdatere den sorterede liste tidligere, sådan så hvis der kommer et track ind, tjekker vi
            // om det allerede er på listen, og i så fald opdaterer vi track'ets attributter. :) 
            currentTracks = e.TrackObjects;
            if (currentTracks.Count >=1)
            {
              HandleTrack();  
            }

            _checker = new CheckForSeparationEvent(currentTracks); // opretter checker her i stedet for at give den med som constructor parameter
            _warningCreator = new WarningDisplay(_checker);

            _checker.SeperationEvents += _checker_SeperationEvents;
            priorTracks = new List<TrackObject>(currentTracks);  
        }

        private void _checker_SeperationEvents(object sender, SeparationEventArgs e) //skal denne være her? 
        {
            _warningCreator.CreateWarning(e);
        }

        public void HandleTrack()
        {
            foreach (var trackC in currentTracks)
            {
                foreach (var trackP in priorTracks)
                {
                    if (trackC.Tag == trackP.Tag)
                    {
                        trackC.horizontalVelocity = ts.CalculateSpeed(trackC, trackP);
                        //tilføj kompaskurs her
                        trackC.compassCourse = tcc.CalculateCompassCourse(trackC, trackP);
                        Console.WriteLine($"{trackC.Tag}, {Convert.ToString(trackC.horizontalVelocity)} m/s, {Convert.ToString(trackC.compassCourse)} deg.");
                    }

                }
            }
        }
    }

        
    }
