using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;

namespace ATM.Logic
{
    public class CreateSeparationEventWarning : ISeperationEventHandler
    {
        private DTO.DTOSeperationEvents DTOSeparationTracks;
        private List<string> warnings = new List<string>();

        public List<String> CreateWarning()
        {
            warnings.Clear();
            if (DTOSeparationTracks.SeparationTracks.Count != 0)
            {
                foreach (var separation in DTOSeparationTracks.SeparationTracks)
                {
                    warnings.Add(separation[0].Tag + " og " + separation[1].Tag + " er i konflikt");
                }
            }

            return warnings;
        }
    }
}
