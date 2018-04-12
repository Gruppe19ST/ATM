using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Interfaces
{
    interface ISorter
    {
        event EventHandler<TrackObjectEventArgs> TrackSortedReady;
        void SortTracks(List<TrackObject> tracks);
    }
}
