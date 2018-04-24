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
        public List<TrackObject> priorTracks;
        private List<TrackObject> currentTracks;
        private ISorter _sorter;
        private ISeperationEventChecker _checker;
        private ISeperationEventHandler _warningCreator;
        private ISeperationEventLogger _logger;
        private ITrackSpeed _ts;
        private ITrackCompassCourse _tcc;

        public Controller(ISorter sorter, ITrackSpeed ts, ITrackCompassCourse tcc, ISeperationEventChecker checker, ISeperationEventHandler warningCreator, ISeperationEventLogger logger)
        {
            currentTracks = new List<TrackObject>();

            _sorter = sorter;
            _sorter.TrackSortedReady += _sorter_TrackSortedReady;

            _ts = ts;
            _tcc = tcc;
           
            _checker = checker;
            _warningCreator = warningCreator;
            _logger = logger;
        }

        private void _sorter_TrackSortedReady(object sender, TrackObjectEventArgs e)
        { 
            currentTracks = e.TrackObjects;
            if (currentTracks.Count >=1)
            {
                HandleTrack(); 
                CheckTracks(currentTracks);
            }
            priorTracks = new List<TrackObject>(currentTracks);
            currentTracks = null;
        }

        public void CheckTracks(List<TrackObject> tracks)
        {
            _checker.CheckSeparationEvents(tracks);
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
                            trackC.horizontalVelocity = _ts.CalculateSpeed(trackC, trackP);
                            trackC.compassCourse = _tcc.CalculateCompassCourse(trackC, trackP);
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
