using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using Newtonsoft.Json;

namespace nhH60Store.Controllers {

    [Route("Product")]
    public class ProductController : Controller {

        [Route("")]
        public async Task<IActionResult> Index() {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                Product product = new Product();
                return View(await product.GetAllProducts());
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [Route("ProductsByCategory")]
        public async Task<IActionResult> ProductsByCategory() {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                Product productsWithCategories = new Product();
                return View(await productsWithCategories.GetAllProductsWithCategories());
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet, Route("Create")]
        public async Task<IActionResult> Create() {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                Product product = new Product();
                ProductCategory categories = new ProductCategory();
                var CategoryList = await categories.GetAllCategories();
                TempData["ProdCatId"] = new SelectList(CategoryList, "CategoryId", "ProdCat");
                return View(product);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(Product product) {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            HttpResponseMessage response = await product.CreateProduct();
            int SCode = (int)response.StatusCode;
            if (SCode == 204) {               
                Product allProduct = new Product();
                TempData["SuccessMessage"] = "Successfully created the product.";
                return RedirectToAction("Index", "Product", await allProduct.GetAllProducts());
            } else if (SCode == 400) {
                TempData["ErrorMessage"] = "Coudldn't create this product. Please check that your databases is linked correctly.";
                return View(product);
            } else if (SCode == 500) {
                TempData["ErrorMessage"] = "Database error. Please check your database connection";
                return View(product);
            }

            return View(product);
        }

        [Route("Details/{id:int}")]
        public async Task<IActionResult> Detail(int id) {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                Product prod = new Product();
                return View(await prod.FindProduct(id));
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet, Route("UpdateStock/{id:int}")]
        public async Task<IActionResult> UpdateStock(int id) {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                Product product = new Product();
                var result = await product.FindProduct(id);
                return View(result);
            } catch(Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }            
        }

        [HttpPost, Route("UpdateStock/{id:int}")]
        public async Task<IActionResult> UpdateStock(Product product) {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                HttpResponseMessage response = await product.UpdateStock();
                int SCode = (int)response.StatusCode;
                if (SCode == 204) {                
                    Product allProduct = new Product();
                    TempData["SuccessMessage"] = "Successfully updated product.";
                    return RedirectToAction("Index", "Product", await allProduct.GetAllProducts());
                } else if (SCode == 404) {
                    TempData["ErrorMessage"] = "Cannot find product in database";
                    return View(product);
                } else if (SCode == 400) {
                    TempData["ErrorMessage"] = "Something went wrong when processing your request.";
                    return View(product);
                } else if (SCode == 500) {
                    TempData["ErrorMessage"] = "Database error.";
                    return View(product);
                }
                return View(product);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View(product);
            }
        }

        [HttpGet, Route("UpdatePrices/{id:int}")]
        public async Task<IActionResult> UpdatePrices(int id) {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                Product product = new Product();
                var result = await product.FindProduct(id);
                return View(result);
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost, Route("UpdatePrices/{id:int}")]
        public async Task<IActionResult> UpdatePrices(Product product) {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            try {
                HttpResponseMessage response = await product.UpdatePrices();
                int SCode = (int)response.StatusCode;
                if (SCode == 204) {
                    Product allProduct = new Product();
                    TempData["SuccessMessage"] = "Successfully updated product category.";
                    return RedirectToAction("Index", "Product", await allProduct.GetAllProducts());
                } else if (SCode == 404) {
                    TempData["ErrorMessage"] = "Cannot find product in database.";
                    return View(product);
                } else if (SCode == 400) {
                    TempData["ErrorMessage"] = "Something went wrong when processing your request.";
                    return View(product);
                } else if (SCode == 500) {
                    TempData["ErrorMessage"] = "Database error.";
                    return View(product);
                }

                return View(product);

            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return View(product);
            }
        }

        [HttpGet, Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            if (User.Identity.IsAuthenticated) {
                return LocalRedirect("/Identity/Account/Login");
            }
            Product prodCat = new Product();
            HttpResponseMessage response = await prodCat.DeleteProduct(id);
            int SCode = (int)response.StatusCode;
            if (SCode == 204) {
                TempData["SuccessMessage"] = "Successfully deleted the product.";
                return RedirectToAction("Index", "Product", await prodCat.GetAllProducts());
            } else if (SCode == 404) {
                TempData["ErrorMessage"] = "Cannot find product in database.";
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