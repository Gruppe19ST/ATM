using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.DTO;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
{
    public class TrackCompassCourse : ITrackCompassCourse
    {
        public double CalculateCompassCourse(TrackObject oldTrackObject, TrackObject newTrackObject)
        {
            //Vector pointing from the planes old position to the planes new position
            CompassCourseVector planeVector = new CompassCourseVector()
            {
                X = newTrackObject.XCoordinate - oldTrackObject.XCoordinate,
                Y = newTrackObject.YCoordinate - oldTrackObject.YCoordinate
            };

            //Vector pointing north. Used to calculate angle the plane is heading
            CompassCourseVector northVector = new CompassCourseVector()
            {
                X = 0,
                Y = planeVector.Magnitude
            };

            var dotProduct = (planeVector.X * northVector.X) + (planeVector.Y * northVector.Y);
            var determinant = (planeVector.X * northVector.X) - (planeVector.Y * northVector.Y);

            //Calculate angle between north and plane vector. 
            var angle = Math.Atan2(determinant, dotProduct) * (180 / Math.PI); //Convert radians to degrees.

            //Convert negative angle values between 0-1 PI to number between 0-360 degrees
            if (angle < 0)
                return Math.Round(360 + angle, 3);

            return Math.Round(angle, 3);
        }
    }
}
