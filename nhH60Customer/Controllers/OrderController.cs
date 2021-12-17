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


        public IActionResult Index() {
            return View();
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

                    foreach (var item in cartItems) {
                        OrderItem oItem = new OrderItem();
                        await oItem.Create(createdOrder.OrderId, item);
                    }

                    var updatedOrder = await order.GetOrderDTO(createdOrder.OrderId);

                    return View("Index", updatedOrder);

                } else {
                    return RedirectToAction("InvalidCreditCard", "Customer", new { id = checkCreditCard });
                }
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Cart", "ShoppingCart");
            }
        }

        [HttpGet, Route("OrderConfirmed/{id}")]
        public async Task<IActionResult> OrderConfirmed(int id) {
            try {

                Order order = new Order();

                var orderToConfirm = await order.GetOrderDTO(id);

                var setDateFulfilled = await order.FinalizeOrder(orderToConfirm);

                var customerShoppingCart = await FindCurrentCustomer();

                ShoppingCart cartObj = new ShoppingCart();

                var cart = await cartObj.GetShoppingCart(customerShoppingCart.CustomerId);

                await cartObj.RemoveCartAndItemsAsync(cart);

                return View();

            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Cart", "ShoppingCart");
            }
        }

    }
}
