using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nhH60Customer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using nhH60Customer.Areas.Identity.Data;

namespace nhH60Customer.Controllers {
    public class HomeController : Controller {

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<nhH60CustomerUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<nhH60CustomerUser> userManager) {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index() {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
