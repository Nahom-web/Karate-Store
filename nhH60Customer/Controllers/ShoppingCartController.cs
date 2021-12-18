using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using nhH60Customer.Models;
using Microsoft.AspNetCore.Identity;
using nhH60Customer.Areas.Identity.Data;
using System.Net.Http;
using nhH60Customer.Dtos;

namespace nhH60Customer.Controllers {
    public class ShoppingCartController : Controller {

        private readonly UserManager<nhH60CustomerUser> _userManager;

        public ShoppingCartController(UserManager<nhH60CustomerUser> userManager) {
            _userManager = userManager;
        }

        private async Task<Customer> FindCurrentCustomer() {
            Customer customer = new Customer();
            var user = await _userManager.GetUserAsync(User);
            var email = _userManager.GetEmailAsync(user);
            return await customer.FindCustomer(email.Result);
        }

        public async Task<IActionResult> Cart() {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                var customerFound = await FindCurrentCustomer();
                ShoppingCart ShoppingCart = new ShoppingCart();
                await ShoppingCart.Create(customerFound);
                ShoppingCartDTO CustomersCart = await ShoppingCart.GetShoppingCart(customerFound.CustomerId);
                if (CustomersCart == null) {
                    HttpResponseMessage response = await ShoppingCart.Create(customerFound);
                    if (response != null) {
                        int SCode = (int)response.StatusCode;
                        if (SCode == 204) {
                            return View(await ShoppingCart.GetShoppingCart(customerFound.CustomerId));
                        } else if (SCode == 404) {
                            TempData["ErrorMessage"] = "Coudldn't create the shopping cart. Please check that your databases is linked correctly.";
                            return View();
                        }
                    }
                }
                return View(CustomersCart);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [HttpGet, Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            ShoppingCart Cart = new ShoppingCart();
            HttpResponseMessage response = await Cart.Delete(id);
            int SCode = (int)response.StatusCode;
            if (SCode == 204) {
                TempData["SuccessMessage"] = "Successfully deleted your shopping cart";
                return RedirectToAction("Index");
            } else if (SCode == 404) {
                TempData["ErrorMessage"] = "Cannot find cart in database";
                return RedirectToAction("Index");
            } else if (SCode == 400) {
                TempData["ErrorMessage"] = "Cannot delete your shopping cart because you have products in it.";
                return RedirectToAction("Index");
            } else if (SCode == 500) {
                TempData["ErrorMessage"] = "Database error.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

    }
}
