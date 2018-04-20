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

        public Controller(ISorter sorter, ISeperationEventChecker checker, ISeperationEventHandler warningCreator)
        {
            _sorter = sorter;
            _checker = checker;
            _warningCreator = warningCreator;

            _sorter.TrackSortedReady += _sorter_TrackSortedReady;
            _checker.SeperationEvents += _checker_SeperationEvents;


            ts = new TrackSpeed();
            tcc = new TrackCompassCourse();
        }

        private void _checker_SeperationEvents(object sender, SeparationEventArgs e)
        {
            _warningCreator.CreateWarning(e);
        }

        private void _sorter_TrackSortedReady(object sender, TrackObjectEventArgs e)
        {
            priorTracks = currentTracks; // Undtaget første gang, da currentracks så vil være tom. Da kopieres currenttracks til priortracks. 
            currentTracks = e.TrackObjects;
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
                    }

                }
            }
        }
    }
}
