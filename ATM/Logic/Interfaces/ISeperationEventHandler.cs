﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Interfaces
{
    public interface ISeperationEventHandler
    {
        List<string> CreateWarning(List<List<TrackObject>> conflictedTracks);
    }
}
