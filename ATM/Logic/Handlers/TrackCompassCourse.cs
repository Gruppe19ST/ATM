using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
{
    public class TrackCompassCourse : ITrackCompassCourse
    {
        private double compassCourse;

        public TrackCompassCourse()
        {
            compassCourse = 0;
        }
        public double CalculateCompassCourse(TrackObject p, TrackObject c)
        {
            //Vector pointing from the planes old position to the planes new position
            CompassCourseVector planeVector = new CompassCourseVector()
            {
                X = c.XCoordinate - p.XCoordinate,
                Y = c.YCoordinate - p.YCoordinate
            };

            //Vector pointing north. Used to calculate the course the plane is heading
            CompassCourseVector northVector = new CompassCourseVector()
            {
                X = 0,
                Y = planeVector.Magnitude
            };

            var dotProduct = (planeVector.X * northVector.X) + (planeVector.Y * northVector.Y);
            var determinant = (planeVector.X * northVector.Y) - (planeVector.Y * northVector.X);

            //Calculate course between north and plane vector. 
            var compassCourse = Math.Atan2(determinant, dotProduct) * (180 / Math.PI); //Convert radians to degrees.

            //Convert negative course values between 0-1 PI to number between 0-360 degrees
            if (compassCourse < 0)
                return Math.Round(360 + compassCourse,3);

            return Math.Round(compassCourse,3);
        }
    }
}
