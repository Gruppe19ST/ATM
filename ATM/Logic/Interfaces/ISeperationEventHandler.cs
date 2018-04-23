using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Handlers;

namespace ATM.Logic.Interfaces
{
    public interface ISeperationEventHandler
    {
        //void CreateSeparationWarning(SeparationEventArgs conflictedTracks);
        void Checker_SeperationEvents(object sender, SeparationEventArgs e);
    }
}
