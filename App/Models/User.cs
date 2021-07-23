using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace com.moosemorals.DWP.Models
{
    public class User : IPoint
    {
        public User(int id, string firstName, string lastName, string email, string iPAddress, double latitude, double longitude, string city)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IPAddress = iPAddress;
            Latitude = latitude;
            Longitude = longitude;
            City = city;
        }

        // TODO: Check requirements to see if Ids really are
        // numeric or strings
        // (The /user/{id} endpoint takes a string, but the
        // user object has id as a number
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("ip_address")]
        public string IPAddress { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }
}
