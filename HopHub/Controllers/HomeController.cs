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

        public IActionResult Index()
        {
            //SearchBeerViewModel searchBeerVM = new SearchBeerViewModel();
            return View();
        }

        public object GetBeer(string beer)
        {
            string key = Configuration["APIKey"];
            string type = "beer";
            string URL = $"https://api.brewerydb.com/v2/search?q={beer}&type={type}&key={key}";

            HttpResponse<string> beerResults = Unirest.get(URL).asJson<string>();

            var results = JsonConvert.DeserializeObject<object>(beerResults.Body);
            return results;
        }

        [HttpPost]
        public IActionResult Index(SearchBeerViewModel searchBeerVM)
        {
            //string query = searchBeerVM.Query;
            //string key = Configuration["APIKey"];
            //string type = "beer";

            //string URL = $"https://api.brewerydb.com/v2/search?q={query}&type={type}&key={key}";

            //HttpResponse<string> beerResults = Unirest.get(URL)
            //    .asJson<string>();

            //var results = JsonConvert.DeserializeObject<object>(beerResults.Body);
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
