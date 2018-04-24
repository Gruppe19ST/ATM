using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Handlers;

namespace ATM.Logic.Interfaces
{
    public interface ISeperationEventLogger
    {
        void Checker_NewSeperationEvents(object sender, SeparationEventArgs e);
    }
}
