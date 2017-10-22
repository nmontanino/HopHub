using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HopHub.Data;
using HopHub.Models;

namespace HopHub.Controllers
{
    public class EntryController : Controller
    {
        private ApplicationDbContext context;

        public EntryController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /Entry/
        public IActionResult Index()
        {
            // TODO: Display list of user entries with rating and comments

            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return Redirect("/Account/Login");
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
                //Beer newBeer = new Beer
                //{
                //    ReferenceID = addEntryVM.BeerID,
                //    AvgRating = addEntryVM.Rating,

                //};

                Entry userEntry = new Entry
                {
                    // TODO: Double check username to userID
                    ApplicationUserID = User.Identity.Name,
                    
                    // String BeerID
                    BeerID = addEntryVM.BeerID,
                    Rating = addEntryVM.Rating,
                    Review = addEntryVM.Review,
                    UserComments = addEntryVM.UserComments,
                    Location = addEntryVM.Location,
                    TimeStamp = DateTime.Now
                };

                // Store new user Entry to the database
                context.Entries.Add(userEntry);
                context.SaveChanges();

                // Ridirect to user log page.
                return Redirect("/Entry");
            }

            // Make sure that the Beer ID property stays with the view model.
            return View(addEntryVM);
        }
    }
}
