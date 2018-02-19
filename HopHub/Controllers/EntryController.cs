using HopHub.Data;
using HopHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HopHub.Controllers
{
    public class EntryController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext context;

        public EntryController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }

        // GET: /Entry/
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get all entries associated with the current user
                string userID = _userManager.GetUserId(HttpContext.User);

                IList<Entry> entries = context
                    .Entries
                    .Include(item => item.Beer)
                    .Where(e => e.ApplicationUserID == userID)
                    .ToList();

                return View(entries);
            }
            return Redirect("/Account/Login");
        }

        // GET: /Entry/Add/
        public IActionResult Add(string id, string name)
        {
            // If user is logged in display form to add a beer to their log.
            if (User.Identity.IsAuthenticated)
            {
                AddEntryViewModel addEntryVM = new AddEntryViewModel();
                addEntryVM.BeerID = id;
                addEntryVM.BeerName = name;

                return View(addEntryVM);
            }
            return Redirect("/Account/Login");
        }

        // POST: /Entry/Add/
        [HttpPost]
        public IActionResult Add(AddEntryViewModel addEntryVM)
        {
            if (ModelState.IsValid)
            {
                // If beer doesnt exist already add to database
                bool exists = context.Beers.Any(b => b.ReferenceID == addEntryVM.BeerID);

                if (!exists)
                {
                    Beer newBeer = new Beer
                    {
                        Name = addEntryVM.BeerName,
                        ReferenceID = addEntryVM.BeerID,
                        AvgRating = null
                    };
                    context.Beers.Add(newBeer);
                    context.SaveChanges();
                }
                
                // Get beer object by reference ID
                Beer existingBeer = context.Beers.Single(b => b.ReferenceID == addEntryVM.BeerID);

                // Create new Entry
                Entry userEntry = new Entry
                {
                    ApplicationUserID = _userManager.GetUserId(HttpContext.User),
                    BeerID = existingBeer.ID,
                    Rating = addEntryVM.Rating,
                    Review = addEntryVM.Review,
                    UserComments = addEntryVM.UserComments,
                    Location = addEntryVM.Location,
                    TimeStamp = DateTime.Now
                };
                
                // Store new user Entry to the database
                context.Entries.Add(userEntry);
                context.SaveChanges();

                // Calculates the average rating for the beer and updates Beers table
                existingBeer.AvgRating = (double)context
                    .Entries
                    .Where(e => e.BeerID == existingBeer.ID)
                    .Sum(e => e.Rating) / context
                    .Entries
                    .Where(e => e.BeerID == existingBeer.ID)
                    .Count();

                context.Beers.Update(existingBeer);
                context.SaveChanges();

                // Ridirect to user log page.
                return Redirect("/Entry");
            }
            return View(addEntryVM);
        }

        public IActionResult Edit(int entryID)
        {
            // TODO: Check if the user has access (is the creator) of the object to be edited

            // Retrieve entry from database by ID
            Entry entryEdit = context.Entries.Single(e => e.ID == entryID);
            
            return View(EditEntryViewModel.EditEntry(entryEdit));
        }

        [HttpPost]
        public IActionResult Edit(EditEntryViewModel editEntryVM)
        {
            if (ModelState.IsValid)
            {
                // Update entry details in database
                Entry entryUpdate = context.Entries.Single(e => e.ID == editEntryVM.ID);

                entryUpdate.Rating = editEntryVM.Rating;
                entryUpdate.UserComments = editEntryVM.UserComments;
                entryUpdate.Review = editEntryVM.Review;
                entryUpdate.Location = editEntryVM.Location;

                context.Entries.Update(entryUpdate);
                context.SaveChanges();

                return Redirect("/Entry");
            }
            return View(editEntryVM);
        }
    }
}
