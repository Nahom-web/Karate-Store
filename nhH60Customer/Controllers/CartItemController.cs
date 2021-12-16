using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using nhH60Customer.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using nhH60Customer.Areas.Identity.Data;
using nhH60Customer.Dtos;

namespace nhH60Customer.Controllers {
    public class CartItemController : Controller {

        private readonly UserManager<nhH60CustomerUser> _userManager;

        public CartItemController(UserManager<nhH60CustomerUser> userManager) {
            _userManager = userManager;
        }

        public IActionResult Index() {
            return View();
        }

        private async Task<Customer> FindCurrentCustomer() {
            Customer customer = new Customer();
            var user = await _userManager.GetUserAsync(User);
            var email = _userManager.GetEmailAsync(user);
            return await customer.FindCustomer(email.Result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CartItem item, IFormCollection form) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            var customerFound = await FindCurrentCustomer();
            ShoppingCart ShoppingCart = new ShoppingCart();
            ShoppingCartDTO CustomersCart = await ShoppingCart.GetShoppingCart(customerFound.CustomerId);
            item.CartId = CustomersCart.CartId;
            item.Price = Convert.ToDecimal(form["SellPrice"]);
            HttpResponseMessage response = await item.Create();
            int SCode = (int)response.StatusCode;
            if (SCode == 204) {
                ShoppingCart cart = new ShoppingCart();
                TempData["SuccessMessage"] = "Added product to your shopping cart.";
                return RedirectToAction("Index", "ShoppingCart", await cart.GetShoppingCarts());
            } else if (SCode == 400) {
                TempData["ErrorMessage"] = "Coudldn't add product to your cart. Please check that your databases is linked correctly.";
                return View(item);
            } else if (SCode == 500) {
                TempData["ErrorMessage"] = "Database error. Please check your database connection";
                return View(item);
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                CartItem cartTime = new CartItem();
                var result = await cartTime.FindCartItem(id);
                return View(result);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CartItemDTO cartItem) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                CartItem item = new CartItem();
                HttpResponseMessage response = await item.Update(cartItem);
                int SCode = (int)response.StatusCode;
                if (SCode == 204) {
                    Product allProduct = new Product();
                    TempData["SuccessMessage"] = "Successfully updated your cart item.";
                    ShoppingCart cart = new ShoppingCart();
                    return RedirectToAction("Index", "ShoppingCart", await cart.GetShoppingCarts());
                } else if (SCode == 404) {
                    TempData["ErrorMessage"] = "Cannot find cart item in database.";
                    return RedirectToAction("Index", "ShoppingCart");
                } else if (SCode == 400) {
                    TempData["ErrorMessage"] = "Something went wrong when processing your request.";
                    return RedirectToAction("Index", "ShoppingCart");
                } else if (SCode == 500) {
                    TempData["ErrorMessage"] = "Database error.";
                    return RedirectToAction("Index", "ShoppingCart");
                }

                return RedirectToAction("Index", "ShoppingCart");

            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            CartItem cartTime = new CartItem();
            HttpResponseMessage response = await cartTime.Delete(id);
            int SCode = (int)response.StatusCode;
            if (SCode == 204) {
                TempData["SuccessMessage"] = "Successfully deleted the cart item.";
                ShoppingCart cart = new ShoppingCart();
                return RedirectToAction("Index", "ShoppingCart", await cart.GetShoppingCarts());
            } else if (SCode == 404) {
                TempData["ErrorMessage"] = "Cannot find cart item in database.";
                return RedirectToAction("Index", "ShoppingCart");
            } else if (SCode == 500) {
                TempData["ErrorMessage"] = "Database error.";
                return RedirectToAction("Index", "ShoppingCart");
            }

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
