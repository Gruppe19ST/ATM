using System;

namespace ATM.Logic.Handlers
{
    class CompassCourseVector
    {
        public double X { get; set; }
        public double Y { get; set; }

        private double magnitude;

        public double Magnitude
        {
            get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)); }
            set { magnitude = value; }
        }
    }
}
