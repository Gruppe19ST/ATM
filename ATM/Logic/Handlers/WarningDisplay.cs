using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
{
    public class WarningDisplay : ISeperationEventHandler
    {
        public WarningDisplay(ISeperationEventChecker checker)
        {
            checker.SeperationEvents += (o, trackArgs) => DisplayWarning(trackArgs);
        }
        
        public void DisplayWarning(SeparationEventArgs conflictList)
        {
            foreach (var separationObject in conflictList.SeparationObjects)
            {
                // $ for at gøre kompile hurtigere
                System.Console.WriteLine($"{separationObject.Tag1} and {separationObject.Tag2} are in conflict");
            }
        }
    }
}
