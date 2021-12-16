using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;
using nhH60Customer.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using nhH60Customer.Dtos;

namespace nhH60Customer.Controllers {
    public class OrderController : Controller {

        private readonly UserManager<nhH60CustomerUser> _userManager;

        public OrderController(UserManager<nhH60CustomerUser> userManager) {
            _userManager = userManager;
        }

        private async Task<Customer> FindCurrentCustomer() {
            Customer customer = new Customer();
            var user = await _userManager.GetUserAsync(User);
            var email = _userManager.GetEmailAsync(user);
            return await customer.FindCustomer(email.Result);
        }


        public async Task<IActionResult> Index() {
            try {
                Order order = new Order();
                var orders = await order.AllOrders();
                return View(orders);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [HttpGet, Route("Create/{id}")]
        public async Task<IActionResult> Create(int id) {
            try {
                var customer = await FindCurrentCustomer();
                var checkCreditCard =  await customer.ValidateCreditCard();
                if (customer.IsValidCreditCard(checkCreditCard)) {
                    Order order = new Order();

                    Order createdOrder = await order.Create(id);

                    ShoppingCart cartObj = new ShoppingCart();

                    var cart = await cartObj.GetShoppingCart(customer.CustomerId);

                    var cartItems = cart.CartItems;

                    var finalizedOrder = await createdOrder.GetOrder();

                    foreach (var item in cartItems) {
                        OrderItem oItem = new OrderItem();
                        await oItem.Create(finalizedOrder.OrderId, item);
                    }

                    return View("Index", finalizedOrder);
                } else {
                    return RedirectToAction("InvalidCreditCard", "Customer", checkCreditCard);
                }
            } catch (Exception e) {
                TempData["ErrorMessage"] = "You deleted your cart or it just cannot be found";
                return View();
            }
        }
    }
}
