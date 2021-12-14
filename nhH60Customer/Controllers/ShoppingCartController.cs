using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using nhH60Customer.Areas.Identity.Data;

namespace nhH60Customer.Controllers {
    public class ShoppingCartController : Controller {

        private readonly UserManager<nhH60CustomerUser> _userManager;

        public ShoppingCartController(UserManager<nhH60CustomerUser> userManager) {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index() {
            try {
                Customer customer = new Customer();
                var user = await _userManager.GetUserAsync(User);
                var email = _userManager.GetEmailAsync(user);
                var customerFound = await customer.FindCustomer(email.Result);
                ShoppingCart cart = new ShoppingCart();
                await cart.Create(customerFound);
                return View(await cart.GetShoppingCart(customerFound.CustomerId));
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ShoppingCart cart) {
            return View();
        }

    }
}
