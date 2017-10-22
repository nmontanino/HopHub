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
                AddEntryViewModel addEntryVM = new AddEntryViewModel();
                addEntryVM.BeerID = id;

                return View(addEntryVM);
            }
            return Redirect("/Account/Login");
        }
        [HttpPost]
        public IActionResult Add(AddEntryViewModel addEntryVM)
        {
            if (ModelState.IsValid)
            {
                Entry userEntry = new Entry
                {
                    // Double check username to userID
                    ApplicationUserID = User.Identity.Name,

                    BeerID = addEntryVM.BeerID,
                    Rating = addEntryVM.Rating,
                    Review = addEntryVM.Review,
                    UserComments = addEntryVM.UserComments,
                    Location = addEntryVM.Location,
                    TimeStamp = DateTime.Now
                };

                // TODO: Store new user Entry to the database

                // Ridirect to user log page.
                return Redirect("/Entry");
            }

            // Make sure that the Beer ID property stays with the view model.
            return View(addEntryVM);
        }
    }
}

