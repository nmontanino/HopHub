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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public EntryController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            context = dbContext;
            this.userManager = userManager;
        }

        // GET: /Entry/
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get all entries associated with the current user
                string userID = userManager.GetUserId(HttpContext.User);

                IList<Entry> entries = context.Entries
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
                
                // Get Beer object by reference ID
                Beer existingBeer = context.Beers
                    .Single(b => b.ReferenceID == addEntryVM.BeerID);

                // Get ApplicationUser by ID of current logged in user
                ApplicationUser user = context.Users
                    .Single(u => u.Id == userManager.GetUserId(HttpContext.User));

                // Create new Entry object
                Entry userEntry = new Entry
                {
                    ApplicationUser = user, 
                    Beer = existingBeer,
                    Rating = addEntryVM.Rating,
                    Review = addEntryVM.Review,
                    UserComments = addEntryVM.UserComments,
                    Location = addEntryVM.Location,
                    TimeStamp = DateTime.Now
                };
                
                // Store new user Entry to the database
                context.Entries.Add(userEntry);
                context.SaveChanges();

                // Calculate the average rating for the beer
                SetAverageRating(existingBeer);

                context.Beers.Update(existingBeer);
                context.SaveChanges();

                // Ridirect to user log page.
                return Redirect("/Entry");
            }
            return View(addEntryVM);
        }
        
        // GET: /Entry/Edit/
        public IActionResult Edit(int entryID)
        {
            //Retrieve entry from database by ID
            Entry entryEdit = context.Entries
                .Include(e => e.Beer)
                .Single(e => e.ID == entryID);

            string userIdOfEntry = entryEdit.ApplicationUserID;
            string currentUserId = userManager.GetUserId(HttpContext.User);

            // Check if the user has access (is the creator) of the object to be edited
            if (User.Identity.IsAuthenticated && userIdOfEntry == currentUserId)
            {
                EditEntryViewModel editEntryVM = EditEntryViewModel.EditEntry(entryEdit);

                return View(editEntryVM);
            }
            return Redirect("/Entry");
        }
        
        // POST: /Entry/Edit/
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

                // Update average rating for the beer
                Beer existingBeer = context.Beers
                    .Single(b => b.ReferenceID == editEntryVM.BeerID);

                SetAverageRating(existingBeer);

                context.Beers.Update(existingBeer);
                context.SaveChanges();

                return Redirect("/Entry");
            }
            return View(editEntryVM);
        }

        #region Helpers

        private void SetAverageRating(Beer beer)
        {
            int sumOfEntries = context.Entries
                    .Where(e => e.BeerID == beer.ID)
                    .Sum(e => e.Rating);

            int numberOfEntries = context.Entries
                    .Where(e => e.BeerID == beer.ID)
                    .Count();

            beer.AvgRating = (double)sumOfEntries / numberOfEntries;
        }

        #endregion
    }
}
