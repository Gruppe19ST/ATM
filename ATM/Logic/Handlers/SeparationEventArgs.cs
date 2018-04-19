using System;
using System.Collections.Generic;

namespace ATM.Logic.Handlers
{
    public class SeparationEventArgs : EventArgs
    {
        public SeparationEventArgs(List<TrackObject> separationList)
        {
            this.SeparationObjects = separationList;
        }

        public List<TrackObject> SeparationObjects { get; }
    }
}
