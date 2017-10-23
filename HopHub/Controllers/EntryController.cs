using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HopHub.Data;
using HopHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                IList<Entry> entries = context
                    .Entries
                    .Where(e => e.ApplicationUserID == _userManager.GetUserId(HttpContext.User))
                    .ToList();

                return View(entries);
            }
            return Redirect("/Account/Login");
        }

        // GET: /Entry/Add/
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

        // POST: /Entry/Add/
        [HttpPost]
        public IActionResult Add(AddEntryViewModel addEntryVM)
        {
            if (ModelState.IsValid)
            {
                Entry userEntry = new Entry
                {
                    ApplicationUserID = _userManager.GetUserId(HttpContext.User),
                    BeerID = addEntryVM.BeerID,  // String BeerID
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
            return View(addEntryVM);
        }
    }
}
