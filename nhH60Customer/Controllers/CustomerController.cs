using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;

namespace nhH60Customer.Controllers {
    public class CustomerController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult InvalidCreditCard(int id) {
            switch (id) {
                case -1:
                    TempData["ErrorMessage"] = "Your credit card has an invalid length";
                    return RedirectToAction("Cart", "ShoppingCart");
                case -2:
                    TempData["ErrorMessage"] = "Your credit card has invalid characters.";
                    return RedirectToAction("Cart", "ShoppingCart");
                case -3:
                    TempData["ErrorMessage"] = "Your credit card is invalid.";
                    return RedirectToAction("Cart", "ShoppingCart");
                case -4:
                    TempData["ErrorMessage"] = "Your credit card's product of last 2 digits must be multiple of 2.";
                    return RedirectToAction("Cart", "ShoppingCart");
                case -5:
                    TempData["ErrorMessage"] = "Your credit card balance is too low to checkout";
                    return RedirectToAction("Cart", "ShoppingCart");
            }
            TempData["ErrorMessage"] = "Your credit card is invalid";
            return RedirectToAction("Cart", "ShoppingCart");
        }
    }
}
