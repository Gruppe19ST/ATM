using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class TrackObjectEventArgs : EventArgs
    {
        public TrackObjectEventArgs(List<TrackObject> trackObjects)
        {
            this.TrackObjects = trackObjects;
        }

        public List<TrackObject> TrackObjects { get; }
    }
}
