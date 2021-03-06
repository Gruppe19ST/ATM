﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Handlers;

namespace ATM.Logic.Interfaces
{
    public interface ITrackReceiver
    {
        void TrackConverter_TrackObjectsReady(object sender, TrackObjectEventArgs e);

        List<TrackObject> GetListOfTrackObjects();
    }
}
