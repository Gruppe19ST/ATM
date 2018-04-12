using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Interfaces
{
    interface ISeperationEventChecker
    {
        List<List<TrackObject>> CheckSeparationEvents();
        void CheckSeperationEvent();
        void SaveToDTO(List<List<TrackObject>> conflictedTracksList);
    }
}
