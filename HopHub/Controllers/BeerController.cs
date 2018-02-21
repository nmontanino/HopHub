using HopHub.Data;
using HopHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using unirest_net.http;

namespace HopHub.Controllers
{
    public class BeerController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public BeerController(ApplicationDbContext dbContext, IConfiguration config)
        {
            context = dbContext;
            configuration = config;
        }

        // Get beer by ID
        public object SingleBeer(string id)
        {
            string key = configuration["APIKey"];
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
                Beer existingBeer = context.Beers
                    .Single(b => b.ReferenceID == id);

                // Get list of entries of that specific beer that includes a review
                IList<Entry> entries = context.Entries
                    .Where(e => e.Beer.ReferenceID == id)
                    .Where(e => e.Review != null)
                    .Include(e => e.ApplicationUser)
                    .ToList();

                existingBeer.Entries = entries;
                return View(existingBeer);
            }
            return View(new Beer());
        }
    }
}

