using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Handlers
{
    class TrackSpeed : Interfaces.ITrackSpeed
    {
        private TimeSpan timeSpan;
        private double horizontalDisplacement;
        private double horizontalVelocity;

        public TrackSpeed()
        {
            horizontalDisplacement = 0;
            horizontalVelocity = 0;
            timeSpan = TimeSpan.Zero;
        }

        public double CalculateSpeed(TrackObject c, TrackObject p)
        {
            timeSpan = c.TimeStamp - p.TimeStamp;
            horizontalDisplacement = Math.Sqrt(Math.Pow((c.XCoordinate - p.XCoordinate), 2) + Math.Pow((c.YCoordinate - p.YCoordinate), 2));
            horizontalVelocity = horizontalDisplacement / timeSpan.TotalSeconds;
            return horizontalVelocity;
        }

        
    }
}
