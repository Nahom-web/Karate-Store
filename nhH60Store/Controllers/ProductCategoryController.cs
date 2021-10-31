using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Store.Models;
using System.Net.Http;

namespace nhH60Store.Controllers {

    [Route("ProductCategory")]
    public class ProductCategoryController : Controller {

        [Route("")]
        public async Task<IActionResult> Index() {
            try {
                ProductCategory pc = new ProductCategory();
                return View(await pc.GetAllCategories());
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [Route("ProductsForCategory/{id:int}")]
        public async Task<IActionResult> ProductsForCategory(int id) {
            try {
                ProductCategory pc = new ProductCategory();
                return View(await pc.GetProductsForCategory(id));
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [HttpGet, Route("Create")]
        public IActionResult Create() {
            try {
                ProductCategory prodCat = new ProductCategory();
                return View(prodCat);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(ProductCategory newProdCat) {
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

        [HttpGet, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(int id) {
            ProductCategory prodCat = new ProductCategory();
            try {
                var result = await prodCat.FindCategory(id);
                return View(result);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [HttpPost, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(ProductCategory updatedProdCat) {
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

        [HttpGet, Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) {
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
                TempData["ErrorMessage"] = "Cannot delete this category becauce there are products in this category.";
                return RedirectToAction("Index");
            } else if (SCode == 500) {
                TempData["ErrorMessage"] = "Database error.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
    }
}