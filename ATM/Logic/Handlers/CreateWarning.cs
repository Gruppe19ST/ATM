using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
{
    public class CreateWarning : ISeperationEventHandler
    {
        public CreateWarning(ISeperationEventChecker checker)
        {
            checker.SeperationEvents += Checker_SeperationEvents;
        }

        public void Checker_SeperationEvents(object sender, SeparationEventArgs e)
{
            CreateSeparationWarning(e);
        }

        public void CreateSeparationWarning(SeparationEventArgs conflictList)
        {
            foreach (var separationObject in conflictList.SeparationObjects)
            {
                // $ for at gøre kompile hurtigere
                System.Console.WriteLine($"{separationObject.Tag1} and {separationObject.Tag2} are in conflict");
            }
        }
    }
}
