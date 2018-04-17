using System;
using System.Collections.Generic;

namespace ATM.Logic.Handlers
{
    public class SeparationEventArgs : EventArgs
    {
        public SeparationEventArgs(List<List<TrackObject>> separationList)
        {
            this.SeparationObjects = separationList;
        }

        public List<List<TrackObject>> SeparationObjects { get; }
    }
}
