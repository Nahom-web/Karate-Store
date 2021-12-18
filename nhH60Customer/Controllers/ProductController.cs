using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using nhH60Customer.Models;


namespace nhH60Customer.Controllers {

    [Route("Product")]

    public class ProductController : Controller {

        [Route("{string?}")]

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public async Task<IActionResult> Index(string? ProductName) {
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
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