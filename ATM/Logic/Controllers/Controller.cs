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
        private ISeperationEventLogger _logger;
        private TrackSpeed ts;
        private TrackCompassCourse tcc;

        public Controller(ISorter sorter)
        {
            currentTracks = new List<TrackObject>();
            _sorter = sorter;
            _sorter.TrackSortedReady += _sorter_TrackSortedReady;
            ts = new TrackSpeed();
            tcc = new TrackCompassCourse();

            _checker = new CheckForSeparationEvent();
            _warningCreator = new CreateWarning(_checker);
            _logger = new LogSeparationEvent(_checker);
        }

        private void _sorter_TrackSortedReady(object sender, TrackObjectEventArgs e)
        { 
            currentTracks = e.TrackObjects;
            if (currentTracks.Count >=1)
            {
                HandleTrack(); 
                CheckTracks(currentTracks);
            }
            // UDSKRIV TRACKS I LUFTEN
            priorTracks = new List<TrackObject>(currentTracks);
            currentTracks = null;
        }

        public void CheckTracks(List<TrackObject> tracks)
        {
            _checker.CheckSeparationEvents(tracks);
            //_checker.SeperationEvents += _checker_SeperationEvents;
        }

        private void _checker_SeperationEvents(object sender, SeparationEventArgs e) //skal denne være her? 
        {
            _checker.CheckSeparationEvents(currentTracks);
            //_warningCreator.CreateSeparationWarning(e);
        }

        public void HandleTrack()
        {
            if (priorTracks != null)
            {
                foreach (var trackC in currentTracks)
                {
                    foreach (var trackP in priorTracks)
                    {
                        if (trackC.Tag == trackP.Tag)
                        {
                            trackC.horizontalVelocity = ts.CalculateSpeed(trackC, trackP);
                            trackC.compassCourse = tcc.CalculateCompassCourse(trackC, trackP);
                            Console.WriteLine(trackC.ToString());
                        }

                    }
                }
            }
            else
            {

                priorTracks = currentTracks;
            }
        }
    }

        
    }
