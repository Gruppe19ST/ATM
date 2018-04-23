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
        // List of tracks in airspace 
        List<TrackObject> priorTracks;
        List<TrackObject> currentTracks;

        // Classes that will handle tracks 
        private ISorter _sorter;
        private ISeperationEventChecker _checker;
        private ISeperationEventHandler _warningCreator;
        private TrackSpeed ts;
        private TrackCompassCourse tcc;

        public Controller(ISorter sorter)
        {
            _sorter = sorter;
            // Register when a list of sorted tracks is ready
            _sorter.TrackSortedReady += _sorter_TrackSortedReady;

            ts = new TrackSpeed();
            tcc = new TrackCompassCourse();
            currentTracks = new List<TrackObject>();
        }

        private void _sorter_TrackSortedReady(object sender, TrackObjectEventArgs e)
        { 
            currentTracks = e.TrackObjects;
            if (currentTracks.Count >=1)
            {
                HandleTrack(); 
                CheckTracks();
            }
            priorTracks = new List<TrackObject>(currentTracks);
            // Set current tracks to null, all information is now contained in prior. 
            currentTracks = null;
        }

        private void CheckTracks()
        {
            _checker = new CheckForSeparationEvent(); 
            _warningCreator = new CreateWarning(_checker);
            _checker.SeperationEvents += _checker_SeperationEvents;
        }

        private void _checker_SeperationEvents(object sender, SeparationEventArgs e)
        {
            _warningCreator.CreateSeparationWarning(e);
        }

        public void HandleTrack()
        { // find speed and compass course of all tracks that are still in monitored air space
            foreach (var trackC in currentTracks)
            {
                foreach (var trackP in priorTracks)
                {
                    if (trackC.Tag == trackP.Tag)
                    {
                        trackC.horizontalVelocity = ts.CalculateSpeed(trackC, trackP);
                        trackC.compassCourse = tcc.CalculateCompassCourse(trackC, trackP);
                    }

                }
            }
        }
    }

        
    }
