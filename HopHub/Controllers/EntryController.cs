using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HopHub.Controllers
{
    public class EntryController : Controller
    {
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
    }
}
