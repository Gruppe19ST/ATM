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
        private ISeperationEventChecker _checker;
        private List<string> warnings = new List<string>();
        private List<List<TrackObject>> _conflictList;

        public CreateSeparationEventWarning(ISeperationEventChecker checker)
        {
            _checker = checker;
            _checker.SeperationEvents += (o, trackArgs) => CreateWarning(trackArgs);
        }
        


        public List<String> CreateWarning(SeparationEventArgs conflictList)
        {
            _conflictList = conflictList.SeparationObjects;
            warnings.Clear();
            if (_conflictList.Count != 0)
            {
                foreach (var separation in _conflictList)
                {
                    warnings.Add(separation[0].Tag + " and " + separation[1].Tag + " are in conflict");
                }
            }

            return warnings;
        }
    }
}
