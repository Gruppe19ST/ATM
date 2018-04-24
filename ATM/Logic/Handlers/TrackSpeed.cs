using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Handlers
{
    public class TrackSpeed : Interfaces.ITrackSpeed
    {
        // Variables 
        private TimeSpan timeSpan;
        private double horizontalDisplacement;
        private double horizontalVelocity;

        public TrackSpeed()
        {
            // Set all to zero
            horizontalDisplacement = 0;
            horizontalVelocity = 0;
            timeSpan = TimeSpan.Zero;
        }

        public double CalculateSpeed(TrackObject c, TrackObject p)
        {
            // How much time has past? 
            timeSpan = c.TimeStamp - p.TimeStamp;
            // How big is the displacement? 
            horizontalDisplacement = Math.Sqrt(Math.Pow((c.XCoordinate - p.XCoordinate), 2) + Math.Pow((c.YCoordinate - p.YCoordinate), 2));
            // velocity = displacement(m)/timespan(sec) 
            horizontalVelocity = horizontalDisplacement / timeSpan.TotalSeconds;
            // round off value and return 
            return Math.Round(horizontalVelocity,2);
        }

        
    }
}
