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
using System.Net.Http;

namespace nhH60Customer.Controllers {
    public class HomeController : Controller {

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<nhH60CustomerUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<nhH60CustomerUser> userManager) {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index() {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            Customer customer = new Customer();
            var user = await _userManager.GetUserAsync(User);
            var email = _userManager.GetEmailAsync(user);
            var customerFound = await customer.FindCustomer(email.Result);
            ShoppingCart cart = new ShoppingCart();
            HttpResponseMessage response = await cart.Create(customerFound);

            if (response != null) {
                int SCode = (int)response.StatusCode;
                if (SCode == 204) {
                    return View();
                } else if (SCode == 404) {
                    TempData["ErrorMessage"] = "Coudldn't create the shopping cart. Please check that your databases is linked correctly.";
                    return View();
                }
            }

            return View();
        }

    }
}
