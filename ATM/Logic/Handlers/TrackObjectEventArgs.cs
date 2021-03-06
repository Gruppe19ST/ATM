﻿using System;
using System.Collections.Generic;

namespace ATM.Logic.Handlers
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
