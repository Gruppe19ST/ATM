﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Interfaces
{
    public interface ITrackConverter
    {
        event EventHandler<TrackObjectEventArgs> TrackObjectsReady;
    }
}