using com.moosemorals.DWP.Lib;
using com.moosemorals.DWP.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace com.moosemorals.DWP.Web.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string apiBase = "https://bpdts-test-app.herokuapp.com/";

        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly Point London = new(51.509865, -0.118092); // From https://www.latlong.net/place/london-the-uk-14153.html

        [HttpGet("users")]
        public async Task<IActionResult> FetchUsers()
        {
            return Ok(await new Fetcher(apiBase, httpClient).FetchUsersAsync("London", London, 50));
        }
    }
}
