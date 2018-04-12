using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Controllers
{
    class Controller : Interfaces.ITrackController
    {
        List<TrackObject> priorTracks;
        List<TrackObject> currentTracks;
        private Interfaces.ISorter _sorter;

        public Controller(Interfaces.ISorter sorter)
        {
            _sorter = sorter;
            _sorter.TrackSortedReady += _sorter_TrackSortedReady;
        }

        private void _sorter_TrackSortedReady(object sender, TrackObjectEventArgs e)
        {
            currentTracks = e.TrackObjects;
        }

        public void HandleTrack()
        {
            throw new NotImplementedException();
        }
    }
}
