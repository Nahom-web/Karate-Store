using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;
using Microsoft.AspNetCore.Http;

namespace nhH60Customer.Controllers {
    public class ShoppingCartController : Controller {

        public async Task<IActionResult> Index() {
            try {
                ShoppingCart cart = new ShoppingCart();
                return View(await cart.GetShoppingCart(HttpContext.Session.GetString("CustomersName")));
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        public async Task<IActionResult> Create() {
            ShoppingCart cart = new ShoppingCart();
            await cart.Create(HttpContext.Session.GetString("CustomersName"));
            return RedirectToAction("Index", "ShoppingCart");
        }

    }
}
