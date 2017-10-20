using System;
using System.Collections.Generic;
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
    public class BeerController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public BeerController(IConfiguration config)
        {
            Configuration = config;
        }

        public object SingleBeer(string id)
        {
            string key = Configuration["APIKey"];
            string uri = $"https://api.brewerydb.com/v2/beer/{id}?withBreweries=Y&key={key}";

            HttpResponse<string> singleBeer = Unirest.get(uri).asJson<string>();

            object result = JsonConvert.DeserializeObject<object>(singleBeer.Body);
            return result;
        }

        // GET: /Beer/
        public IActionResult Index(string id)
        {
            // Get Single beer by ID and display information.
            string key = Configuration["APIKey"];
            string uri = $"https://api.brewerydb.com/v2/beer/{id}?withBreweries=Y&key={key}";

            HttpResponse<string> singleBeer = Unirest.get(uri).asJson<string>();

            ViewBag.json = JsonConvert.DeserializeObject<object>(singleBeer.Body);  

            return View();
        }
    }
}

