using System;
using System.Collections.Generic;
using System.IO;
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

        // Boolean to check whether there's a console and something to clear or not
        private bool consoleReady;

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

            consoleReady = false;
        }

        private void _sorter_TrackSortedReady(object sender, TrackObjectEventArgs e)
        {
            currentTracks = e.TrackObjects;
            if (currentTracks.Count >= 1)
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

        /*
         * Comment regarding the clearing of Console:
         * We don't know, if it's okay to do it this way?
         * We've tried multiple ways to checking, that there is a console to clear (as this failed in unit testing)
         * and we found this to work.
         * Hopefully it's allowed to do it that way?
         */
        public void HandleTrack()
        {
            // If there's already a console, something might be in it - clear this
            if (Console.In.GetLifetimeService() != null)
            {
                if (Console.CursorVisible)
                {
                    Console.Clear();
                }
            }

            if (currentTracks != null)
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
                                // Make sure that the color is reset as it might be red from a conflict
                                Console.ResetColor();
                                Console.WriteLine(trackC.ToString());
                                
                                Console.In.InitializeLifetimeService();
                            }

                        }
                    }
                }
                else
                {
                    foreach (var trackC in currentTracks)
                    {
                        // Make sure that the color is reset as it might be red from a conflict
                        Console.ResetColor();
                        Console.WriteLine(trackC.ToString());

                        Console.In.InitializeLifetimeService();
                    }
                }
            }
        }
    }


}
