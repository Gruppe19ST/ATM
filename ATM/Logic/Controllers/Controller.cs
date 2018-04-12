using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Handlers;

namespace ATM.Logic.Controllers
{
    class Controller : Interfaces.ITrackController
    {
        List<TrackObject> priorTracks;
        List<TrackObject> currentTracks;
        private Interfaces.ISorter _sorter;
        private TrackSpeed ts;

        public Controller(Interfaces.ISorter sorter)
        {
            _sorter = sorter;
            _sorter.TrackSortedReady += _sorter_TrackSortedReady;
            ts = new TrackSpeed();
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
                    }

                }
            }
        }
    }
}
