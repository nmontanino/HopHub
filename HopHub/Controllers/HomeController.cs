﻿using HopHub.Data;
using HopHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using unirest_net.http;

namespace HopHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public HomeController(ApplicationDbContext dbContext, IConfiguration config)
        {
            context = dbContext;
            configuration = config;
        }
        public async Task<object> SearchBeers(string beer, string pageNum = "1")
        {
            string type = "beer";
            string key = configuration["APIKey"];
            string uri = $"https://api.brewerydb.com/v2/search?p={pageNum}&q={beer}&type={type}&withBreweries=Y&key={key}";

            HttpResponse<string> beerResults = await Unirest.get(uri).asJsonAsync<string>();

            var results = JsonConvert.DeserializeObject<object>(beerResults.Body);
            return results;
        }
        /*
        public object GetBeer(string beer, string pageNum = "1")
        {
            string type = "beer";
            string key = configuration["APIKey"];
            string uri = $"https://api.brewerydb.com/v2/search?p={pageNum}&q={beer}&type={type}&withBreweries=Y&key={key}";

            HttpResponse<string> beerResults = Unirest.get(uri).asJson<string>();

            var results = JsonConvert.DeserializeObject<object>(beerResults.Body);
            return results;
        }

        public object GetFeatured()
        {
            string key = configuration["APIKey"];
            string uri = $"https://api.brewerydb.com/v2/featured?key={key}";

            HttpResponse<string> featuredBeers = Unirest.get(uri).asJson<string>();

            var results = JsonConvert.DeserializeObject<object>(featuredBeers.Body);
            return results;
        }
        */
        public object GetBrewery(string id)
        {
            string key = configuration["APIKey"];
            string uri = $"https://api.brewerydb.com/v2/brewery/{id}?key={key}";

            HttpResponse<string> brewery = Unirest.get(uri).asJson<string>();

            var results = JsonConvert.DeserializeObject<object>(brewery.Body);
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
            IList<Entry> recentReviews = context.Entries
                .Where(e => e.Review != null)
                .OrderByDescending(e => e.TimeStamp)
                .Include(e => e.Beer)
                .Include(e => e.ApplicationUser)
                .Take(5)
                .ToList();

            ViewBag.recent = recentReviews;
            return View(topRated);
        }

        public IActionResult Search()
        {
            // Display search results
            return View();
        }

        public IActionResult Brewery()
        {
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
