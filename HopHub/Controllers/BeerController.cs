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
using HopHub.Data;

namespace HopHub.Controllers
{
    public class BeerController : Controller
    {
        public IConfiguration Configuration { get; set; }
        private ApplicationDbContext context;

        public BeerController(IConfiguration config, ApplicationDbContext dbContext)
        {
            Configuration = config,
            context = dbContext
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
            // Displays information of a single beer (add to DB if not there already)
            
            return View();
        }
    }
}

