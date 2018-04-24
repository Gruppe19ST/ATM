using System;
using System.Collections.Generic;

namespace ATM.Logic.Handlers
{
    public class SeparationEventArgs : EventArgs
    {
        public SeparationEventArgs(SeparationEventObject separationObject)
        {
            this.SeparationObject = separationObject;
        }

        public SeparationEventArgs(List<SeparationEventObject> separationList)
        {
            this.SeparationObjects = separationList;
        }

        public List<SeparationEventObject> SeparationObjects { get; }
        public SeparationEventObject SeparationObject { get; }
    }
}
