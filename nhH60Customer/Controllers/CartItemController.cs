using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;

namespace nhH60Customer.Controllers {
    public class CartItemController : Controller {
        public IActionResult Index() {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> Create() {
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(CartItem item) {
        //    return View();
        //}
    }
}
