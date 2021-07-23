using com.moosemorals.DWP.Lib;
using com.moosemorals.DWP.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace com.moosemorals.DWP
{
    public class Program
    {
        private const string apiBase = "https://bpdts-test-app.herokuapp.com/";
        private const double earthRadius = 3958; // miles

        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly Point London = new(51.509865, -0.118092); // From https://www.latlong.net/place/london-the-uk-14153.html

        public static async Task Main(string[] args)
        {
            IEnumerable<User> users = await FetchUsersAsync("London", London, 50);

            string json = JsonConvert.SerializeObject(users.OrderBy(u => u.Id), Formatting.Indented);

            using StreamWriter stdOut = new StreamWriter(Console.OpenStandardOutput());
            stdOut.WriteLine(json);
        }

        private static async Task<IEnumerable<User>> FetchUsersAsync(string City, Point Center, double Radius)
        {
            Fetcher api = new Fetcher(apiBase, httpClient);

            // Fetch all users
            IEnumerable<User> usersByDistance = (await api.GetAllUsersAsync())
                // Within 50 miles of London
                .Where(u => Sphere.Distance(earthRadius, Center, u) < Radius);

            // Fetch users from London
            IEnumerable<User> usersByCity = await api.GetUsersInCityAsync(City);

            // Combine lists
            return usersByCity.Concat(usersByDistance)
                    // Remove duplicates
                    .Distinct();
        }

    }
}
