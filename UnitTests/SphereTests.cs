using System;
using System.Collections;
using System.Collections.Generic;

using com.moosemorals.DWP.Lib;
using com.moosemorals.DWP.Models;

using NUnit.Framework;

namespace Unit_Tests {
    public class SphereTests {

        // X = Longitude
        // Y = Latitude

        private const double _radiusEarth = 3958; // miles

        [TestCaseSource(nameof(UnitSphereTestCases))]
        public void UnitSphereTests(double lat, double lon, double expected, string description) {

            double _delta = 1e-8; // Should be at least as accurate as the user data

            Point origin = new(0, 0);

            Point target = new(lat, lon);

            double actual = Sphere.Distance(1, origin, target);

            Assert.AreEqual(expected, actual, _delta, description);

        }

        // Easy to calculate manually
        public static readonly object[][] UnitSphereTestCases = new object[][] {

            new object[] { 0, 90, Math.PI / 2, "North Pole" },
            new object[] { 0, -90, Math.PI / 2, "South Pole" },
            new object[] { 90, 0, Math.PI / 2, "East Pole" },
            new object[] { -90, 0, Math.PI / 2, "West Pole" },

        };

        // Use data from the real world to check our maths
        [TestCaseSource(typeof(EarthDataTestCases))]
        public void EarthDataSphereTests(Point from, Point to, double expected, string description) {

            double _delta = _radiusEarth * 0.01; // Our data here is a bit sloppy, so anything within 1% is fine

            double actual = Sphere.Distance(_radiusEarth, from, to);

            Assert.AreEqual(expected, actual, _delta, description);
        }

        // Build up a set of test cases from the data below
        public class EarthDataTestCases : IEnumerable {
            public IEnumerator GetEnumerator() {

                foreach (CityPair p in _cityPairs) {

                    Point from = _cities[p.From];
                    Point to = _cities[p.To];

                    // Might as well check both directions
                    yield return new object[] { from, to, p.Distance, $"{p.From} to {p.To}" };
                    yield return new object[] { to, from, p.Distance, $"{p.To} to {p.From}" };
                }
            }
        }
 
        // A selection of cities, with at least one each North/South and East/West
        public static readonly Dictionary<string, Point> _cities = new() {
            { "Tokyo", new(35.6897, 139.6922) },
            { "Shanghai", new(31.2286, 121.4747) },
            { "São Paulo", new(-23.55, -46.6333) },
            { "Lagos", new(6.4550, 3.384) },
            { "Pretoria", new(-25.7461, 28.1880) },
            { "Chicago", new(41.8819, -87.6277) },
        };

        public record CityPair(string From, string To, double Distance);

        // Actual distance between pairs of cities
        // Data from https://www.distancefromto.net/
        public static readonly List<CityPair> _cityPairs = new() {
            new("Tokyo", "Shanghai", 1098),
            new("Tokyo", "São Paulo", 11515),
            new("Tokyo", "Lagos", 8376),
            new("Tokyo", "Pretoria", 8393),
            new("Tokyo", "Chicago", 6297),

            new("Shanghai", "São Paulo", 11534),
            new("Shanghai", "Lagos", 7597),
            new("Shanghai", "Pretoria", 7295),
            new("Shanghai", "Chicago", 7057),

            new("São Paulo", "Lagos", 3958),
            new("São Paulo", "Pretoria", 4633),
            new("São Paulo", "Chicago", 5224),

            new("Lagos", "Pretoria", 2778),
            new("Lagos", "Chicago", 5973),

            new("Pretoria", "Chicago", 8677)
        };
    }
}