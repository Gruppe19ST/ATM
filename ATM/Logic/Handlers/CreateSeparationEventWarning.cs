using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
{
    public class CreateSeparationEventWarning : ISeperationEventHandler
    {
        public CreateSeparationEventWarning(ISeperationEventChecker checker)
        {
            checker.SeperationEvents += (o, trackArgs) => CreateWarning(trackArgs);
        }
        
        public string CreateWarning(SeparationEventArgs conflictList)
        {
            // $ for at gøre kompile hurtigere
            return $"{conflictList.SeparationObjects[0].Tag} and {conflictList.SeparationObjects[1].Tag} are in conflict";
        }
    }
}
