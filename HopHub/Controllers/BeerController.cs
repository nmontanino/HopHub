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

        // Get beer by ID
        public object SingleBeer(string id)
        {
            string key = Configuration["APIKey"];
            string uri = $"https://api.brewerydb.com/v2/beer/{id}?withBreweries=Y&key={key}";

            HttpResponse<string> singleBeer = Unirest.get(uri).asJson<string>();

            object result = JsonConvert.DeserializeObject<object>(singleBeer.Body);
            return result;
        }

        // GET: /Beer/
        public IActionResult Index()
        {
            // Displays information of a single beer
            return View();
        }
        public IActionResult Add(string id)
        {
            // If user is logged in display form to add a beer to their log.
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return Redirect("/Account/Login");
        }
    }
}

