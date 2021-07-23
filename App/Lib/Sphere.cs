using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using com.moosemorals.DWP.Models;

namespace com.moosemorals.DWP.Lib
{

    /// <summary>
    /// A class to hold methods for calculations of geodisics
    /// </summary>
    public static class Sphere
    {
        /// <summary>
        /// Calculate the great circle distance between two points on a sphere
        /// </summary>
        /// <param name="radius">Radius of the sphere</param>
        /// <param name="a">Start point, with lat/lon in decimal degrees</param>
        /// <param name="b">End point, with lat/lon in decimal degrees</param>
        /// <returns>Distance from a to b in the same units as radius</returns>
        // See https://en.wikipedia.org/wiki/Haversine_formula
        public static double Distance(double radius, IPoint a, IPoint b)
        {
            // Step one, convert a and b into radians
            Point aRad = new(ToRadians(a.Latitude), ToRadians(a.Longitude));
            Point bRad = new(ToRadians(b.Latitude), ToRadians(b.Longitude));

            // Step two, calculate haversine formula
            double h = Haversine(bRad.Latitude - aRad.Latitude) +
                Math.Cos(aRad.Latitude)
                * Math.Cos(bRad.Latitude)
                * Haversine(bRad.Longitude - aRad.Longitude);

            // Check that the value we have is between 0 and 1
            // TODO: Handle this more gracefully
            Debug.Assert(0 <= h && h <= 1);

            // Step 3, Solve for distance

            return 2 * radius * Math.Asin(Math.Sqrt(h));
        }

        /// <summary>
        /// Cacluate Haversine of t (in radians).
        /// </summary>
        /// <param name="t">Angle in radians</param>
        /// <returns></returns>
        private static double Haversine(double t) => (1 - Math.Cos(t)) / 2;

        private static double ToRadians(double t) => Math.PI * t / 180;

    }
}
