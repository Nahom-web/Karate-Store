using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Store.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace nhH60Store.Controllers {

    [Route("Customer")]

    public class CustomerController : Controller {

        [Authorize(Roles = "manager, clerk")]
        [Route("")]
        public async Task<IActionResult> Index() {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                Customer customer = new Customer();
                var c = await customer.GetAllCustomers();
                return View(c);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [Authorize(Roles = "manager, clerk")]
        [HttpGet, Route("Create")]
        public async Task<IActionResult> Create() {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                Customer customer = new Customer();
                return View(customer);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [Authorize(Roles = "manager, clerk")]
        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(Customer newCustomer) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            if (ModelState.IsValid){
                HttpResponseMessage response = await newCustomer.Create();
                int SCode = (int)response.StatusCode;
                if (SCode == 204) {
                    Customer customer = new Customer();
                    TempData["SuccessMessage"] = "Successfully created customer";
                    return RedirectToAction("Index", "Customer", await customer.GetAllCustomers());
                } else if (SCode == 400) {
                    TempData["ErrorMessage"] = "Coudldn't create customer. Please check that your databases is linked correctly.";
                    return View(newCustomer);
                } else if (SCode == 500) {
                    TempData["ErrorMessage"] = "Database error. Please check your database connection";
                    return View(newCustomer);
                }
                return View(newCustomer);
            }
            return View(newCustomer);
        }

        [Authorize(Roles = "manager, clerk")]
        [Route("Detail/{id:int}")]
        public async Task<IActionResult> Detail(int id) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                Customer cust = new Customer();
                return View(await cust.FindCustomer(id));
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "manager, clerk")]
        [HttpGet, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(int id) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            Customer customer = new Customer();
            try {
                var result = await customer.FindCustomer(id);
                return View(result);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [Authorize(Roles = "manager, clerk")]
        [HttpPost, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(Customer updatedCustomer) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                HttpResponseMessage response = await updatedCustomer.Update();
                int SCode = (int)response.StatusCode;
                if (SCode == 204) {
                    Customer customer = new Customer();
                    TempData["SuccessMessage"] = "Successfully updated customer.";
                    return RedirectToAction("Index", "Customer", await customer.GetAllCustomers());
                } else if (SCode == 404) {
                    TempData["ErrorMessage"] = "Cannot find customer in database";
                    return View(updatedCustomer);
                } else if (SCode == 400) {
                    TempData["ErrorMessage"] = "Something went wrong when processing your request.";
                    return View(updatedCustomer);
                } else if (SCode == 500) {
                    TempData["ErrorMessage"] = "Database error.";
                    return View(updatedCustomer);
                }
                return View(updatedCustomer);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View(updatedCustomer);
            }
        }

        [Authorize(Roles = "manager, clerk")]
        [HttpGet, Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            Customer customer = new Customer();
            HttpResponseMessage response = await customer.Delete(id);
            int SCode = (int)response.StatusCode;
            if (SCode == 204) {
                TempData["SuccessMessage"] = "Successfully deleted customer";
                return RedirectToAction("Index", "Customer", await customer.GetAllCustomers());
            } else if (SCode == 404) {
                TempData["ErrorMessage"] = "Cannot find customer in database";
                return RedirectToAction("Index");
            } else if (SCode == 400) {
                TempData["ErrorMessage"] = "Sorry, cannot delete account because there are orders and/or items in the customer's shopping cart.";
                return RedirectToAction("Index");
            } else if (SCode == 500) {
                TempData["ErrorMessage"] = "Database error.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }


        //private LocalRedirectResult CheckIfLoggedIn() {
        //    if (User.Identity.IsAuthenticated) {
        //        return LocalRedirect("/Identity/Account/Login");
        //    }
        //}
    }
}
