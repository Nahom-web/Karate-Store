using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Store.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace nhH60Store.Controllers {

    [Route("ProductCategories")]
    public class ProductCategoryController : Controller {

        [Authorize(Roles = "manager, clerk")]
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

        [Authorize(Roles = "manager, clerk")]
        [Route("ProductsForCategory/{id:int}")]
        public async Task<IActionResult> ProductsForCategory(int id) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                ProductCategory pc = new ProductCategory();
                return View(await pc.GetProductsForCategory(id));
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [Authorize(Roles = "manager, clerk")]
        [HttpGet, Route("Create")]
        public IActionResult Create() {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                ProductCategory prodCat = new ProductCategory();
                return View(prodCat);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [Authorize(Roles = "manager, clerk")]
        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(ProductCategory newProdCat) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            HttpResponseMessage response = await newProdCat.Create();
            int SCode = (int)response.StatusCode;
            if (SCode == 204) {
                ProductCategory allCategories = new ProductCategory();
                TempData["SuccessMessage"] = "Successfully created product category.";
                return RedirectToAction("Index", "ProductCategory", await allCategories.GetAllCategories());
            } else if (SCode == 400) {
                TempData["ErrorMessage"] = "Coudldn't create product category. Please check that your databases is linked correctly.";
                return View(newProdCat);
            } else if (SCode == 500) {
                TempData["ErrorMessage"] = "Database error. Please check your database connection";
                return View(newProdCat);
            }

            return View(newProdCat);
        }

        [Authorize(Roles = "manager, clerk")]
        [HttpGet, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(int id) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            ProductCategory prodCat = new ProductCategory();
            try {
                var result = await prodCat.FindCategory(id);
                return View(result);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [Authorize(Roles = "manager, clerk")]
        [HttpPost, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(ProductCategory updatedProdCat) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                HttpResponseMessage response = await updatedProdCat.Update();
                int SCode = (int)response.StatusCode;
                if (SCode == 204) {
                    ProductCategory allCategories = new ProductCategory();
                    TempData["SuccessMessage"] = "Successfully updated product category.";
                    return RedirectToAction("Index", "ProductCategory", await allCategories.GetAllCategories());
                } else if (SCode == 404) {
                    TempData["ErrorMessage"] = "Cannot find product cateogry in database";
                    return View(updatedProdCat);
                } else if (SCode == 400) {
                    TempData["ErrorMessage"] = "Something went wrong when processing your request.";
                    return View(updatedProdCat);
                } else if(SCode == 500) {
                    TempData["ErrorMessage"] = "Database error.";
                    return View(updatedProdCat);
                }

                return View(updatedProdCat);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View(updatedProdCat);
            }
        }

        [Authorize(Roles = "manager, clerk")]
        [HttpGet, Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            if (!User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            ProductCategory prodCat = new ProductCategory();
            HttpResponseMessage response = await prodCat.Delete(id);
            int SCode = (int)response.StatusCode;
            if (SCode == 204) {
                TempData["SuccessMessage"] = "Successfully deleted product category";
                return RedirectToAction("Index", "ProductCategory", await prodCat.GetAllCategories());
            } else if (SCode == 404) {
                TempData["ErrorMessage"] = "Cannot find product cateogry in database";
                return RedirectToAction("Index");
            } else if (SCode == 400) {
                TempData["ErrorMessage"] = "Cannot delete this category because there are products in this category.";
                return RedirectToAction("Index");
            } else if (SCode == 500) {
                TempData["ErrorMessage"] = "Database error.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
    }
}