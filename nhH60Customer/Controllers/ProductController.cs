using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;


namespace nhH60Customer.Controllers {

    [Route("Product")]

    public class ProductController : Controller {

        [Route("{string?}")]

        public async Task<IActionResult> Index(string? ProductName) {
            try {
                Product product = new Product();
                if (ProductName == null)
                    return View(await product.GetAllProducts());
                return View(await product.FindProduct(ProductName));
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }
    }
}