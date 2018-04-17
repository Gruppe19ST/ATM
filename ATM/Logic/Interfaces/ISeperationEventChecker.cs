﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Handlers;

namespace ATM.Logic.Interfaces
{
    public interface ISeperationEventChecker
    {
        event EventHandler<SeparationEventArgs> SeperationEvents;
        // List<List<TrackObject>> CheckSeparationEvents();
        void CheckSeparationEvents();
    }
}
