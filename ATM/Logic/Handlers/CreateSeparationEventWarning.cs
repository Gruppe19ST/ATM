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
        private List<string> warnings = new List<string>();


        public List<String> CreateWarning(List<List<TrackObject>> conflictList)
        {
            warnings.Clear();
            if (conflictList.Count != 0)
            {
                foreach (var separation in conflictList)
                {
                    warnings.Add(separation[0].Tag + " og " + separation[1].Tag + " er i konflikt");
                }
            }

            return warnings;
        }
    }
}
