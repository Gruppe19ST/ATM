﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Interfaces
{
    interface ITrackSorter
    {
        void SortTracks(List<TrackObject> listOfTrackObjects);
        void SaveToDTO();
    }
}