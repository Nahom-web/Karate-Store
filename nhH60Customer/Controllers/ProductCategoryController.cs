﻿using Microsoft.AspNetCore.Mvc;
using nhH60Customer.Models;
using System;
using System.Threading.Tasks;

namespace nhH60Customer.Controllers {

    [Route("ProductCategory")]

    public class ProductCategoryController : Controller {


        [Route("")]

        public async Task<IActionResult> Index() {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                ProductCategory pc = new ProductCategory();
                return View(await pc.GetAllCategories());
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }
    }
}
