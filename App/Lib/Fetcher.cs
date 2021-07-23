using com.moosemorals.DWP.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace com.moosemorals.DWP.Lib
{
    public class Fetcher
    {
        private readonly string apiBase;
        private readonly HttpClient httpClient;

        public Fetcher(string ApiBase, HttpClient HttpClient)
        {
            apiBase = ApiBase;
            httpClient = HttpClient;
        }

        /// <summary>
        /// Hit the '/users' endpoint to get all users
        /// </summary>
        /// <returns>A list of users</returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await ParseResponse(await httpClient.GetAsync(BuildUri("/users")));
        }

        /// <summary>
        /// Hit the '/city/{name}/users' endpoint to get users in a city
        /// </summary>
        /// <param name="city"></param>
        /// <returns>A list of users</returns>
        public async Task<List<User>> GetUsersInCityAsync(string city)
        {
            return await ParseResponse(await httpClient.GetAsync(BuildUri($"/city/{city}/users")));
        }

        /// <summary>
        /// Parse the API response from JSON to a list of users
        /// </summary>
        /// <param name="response">A HttpResponseMessage from the API</param>
        /// <returns>A list of users</returns>
        private static async Task<List<User>> ParseResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>>(json) ?? throw new JsonException("Unexpected response");
            }

            throw new Exception($"Network Error: {response.StatusCode} {response.ReasonPhrase}");
        }

        public Uri BuildUri(string path)
        {
            return new UriBuilder(apiBase)
            {
                Path = path
            }.Uri;
        }
    }
}
