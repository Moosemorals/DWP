using com.moosemorals.DWP.Lib;
using com.moosemorals.DWP.Models;

using Newtonsoft.Json;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests
{
    public class FetcherTests
    {

        private const string apiBase = "https://example.com/";

        [Test]
        public async Task Fetch_Users()
        {
            // Arrange
            string body = JsonConvert.SerializeObject(users);
            List<User> expected = JsonConvert.DeserializeObject<List<User>>(body);
            Fetcher f = new Fetcher(apiBase, GetHttpClient(body));

            // Act
            List<User> actual = await f.GetAllUsersAsync();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Fetch_City()
        {
            // Arrange
            string targetCity = "Town";

            List<User> u = users.Where(u => u.City == targetCity).ToList();
            string body = JsonConvert.SerializeObject(u);
            List<User> expected = JsonConvert.DeserializeObject<List<User>>(body);

            Fetcher f = new Fetcher(apiBase, GetHttpClient(body));

            // Act
            List<User> actual = await f.GetUsersInCityAsync(targetCity);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Build_Uri()
        {
            Fetcher f = new Fetcher(apiBase, null);

            Uri result = f.BuildUri("/test");

            Assert.AreEqual($"{apiBase}test", result.ToString());
        }


        private static readonly List<User> users = new List<User>() {
            new User(1, "John", "Random", "randomJ@example.com", "127.0.0.2", 0, 0, "City"),
            new User(1, "Joan", "Random", "joan.random@example.com", "127.0.0.4", 0, 0, "Town"),
        };

        private HttpClient GetHttpClient(string body)
        {
            return new HttpClient(new HttpRequestFaker(body), true);
        }


        /// <summary>
        /// A HttpMessageHandler that returns the string passed to it's constructor
        /// </summary>
        private class HttpRequestFaker : HttpMessageHandler
        {
            private readonly string body;
            public HttpRequestFaker(string Body)
            {
                body = Body;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {

                HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(body, Encoding.UTF8, "application/json"),
                };

                return Task.FromResult(responseMessage);
            }
        }

    }
}
