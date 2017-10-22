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
        public IConfiguration Configuration { get; set; }

        public HomeController(IConfiguration config)
        {
            Configuration = config;
        }

        public object GetBeer(string beer)
        {
            string key = Configuration["APIKey"];
            string uri = $"https://api.brewerydb.com/v2/search?q={beer}&type=beer&withBreweries=Y&key={key}";

            HttpResponse<string> beerResults = Unirest.get(uri).asJson<string>();

            var results = JsonConvert.DeserializeObject<object>(beerResults.Body);
            return results;
        }

        public IActionResult Index()
        {
            // Display search bar
            return View();
        }

        public IActionResult Search()
        {
            // Display search results
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
