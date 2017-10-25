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
using Microsoft.EntityFrameworkCore;

namespace HopHub.Controllers
{
    public class BeerController : Controller
    {
        private ApplicationDbContext context;

        public IConfiguration Configuration { get; set; }

        public BeerController(ApplicationDbContext dbContext, IConfiguration config)
        {
            context = dbContext;
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
        // Displays information of a single beer
        public IActionResult Index(string id)
        {
            bool beerExists = context.Beers.Any(b => b.ReferenceID == id);

            if (beerExists)
            {
                // If beer already in db get beer object by ID
                Beer existingBeer = context
                    .Beers
                    .Single(b => b.ReferenceID == id);

                // Get list of entries of that specific beer that includes a review
                IList<Entry> entries = context
                    .Entries
                    .Where(e => e.Beer.ReferenceID == id)
                    .Where(e => e.Review != null)
                    .ToList();

                existingBeer.Entries = entries;
                return View(existingBeer);
            }
            return View(new Beer());
        }
    }
}

