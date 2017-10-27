using HopHub.Data;
using HopHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using unirest_net.http;

namespace HopHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context;
        public IConfiguration Configuration { get; set; }

        public HomeController(ApplicationDbContext dbContext, IConfiguration config)
        {
            context = dbContext;
            Configuration = config;
        }

        public object GetBeer(string beer, string type="beer")
        {
            string key = Configuration["APIKey"];
            string uri = $"https://api.brewerydb.com/v2/search?q={beer}&type={type}&withBreweries=Y&key={key}";

            HttpResponse<string> beerResults = Unirest.get(uri).asJson<string>();

            var results = JsonConvert.DeserializeObject<object>(beerResults.Body);
            return results;
        }

        public IActionResult Index()
        {
            // Get list of beer ordered by highest average rating
            IList<Beer> topRated = context.Beers
                .OrderByDescending(b => b.AvgRating)
                .Take(5)
                .Include(b => b.Entries)
                .ToList();

            // Get list of entries sorted by time
            IList<Entry> recentReviews = context
                .Entries
                .Where(e => e.Review != null)
                .OrderByDescending(e => e.TimeStamp)
                .Include(e => e.Beer)
                .Take(3)
                .ToList();

            ViewBag.recent = recentReviews;
            return View(topRated);
        }

        public IActionResult Search()
        {
            // Display search results
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
