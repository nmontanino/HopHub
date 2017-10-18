using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HopHub.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using unirest_net.http;
using Microsoft.Extensions.Configuration;

namespace HopHub.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }

        public HomeController(IConfiguration config)
        {
            Configuration = config;
        }

        public IActionResult Index()
        {
            string key = Configuration["APIKey"];
            string query = "Golden+Monkey";
            string type = "beer";

            string testURL = $"https://api.brewerydb.com/v2/search?q={query}&type={type}&key={key}";

            HttpResponse<string> jsonResponse = Unirest.get(testURL)
                .asJson<string>();

            var json = JsonConvert.DeserializeObject<object>(jsonResponse.Body);
            ViewBag.json = json;

            return View();
        }

        public IActionResult Search()
        {
            // TODO: Set up search bar that takes in query parameter
            return View();
        }

        [HttpPost]
        public IActionResult Search(string query)
        {
            // TODO: Search should return list of beers 
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
