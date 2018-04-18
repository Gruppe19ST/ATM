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
        private List<TrackObject> _conflictList;

        public CreateSeparationEventWarning(ISeperationEventChecker checker)
        {
            _checker = checker;
            _checker.SeperationEvents += (o, trackArgs) => CreateWarning(trackArgs);
        }
        
        public String CreateWarning(SeparationEventArgs conflictList)
        {
            _conflictList = conflictList.SeparationObjects;
            return _conflictList[0].Tag + " and " + _conflictList[1].Tag + " are in conflict";
        }
    }
}
