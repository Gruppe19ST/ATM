using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic
{
    public class SeparationEventObject
    {
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public DateTime FirstTime { get; set; }
        public DateTime LastTime { get; set; }

        public SeparationEventObject(string tag1, string tag2, DateTime first, DateTime last)
        {
            Tag1 = tag1;
            Tag2 = tag2;
            FirstTime = first;
            LastTime = last;
        }

    }
}
