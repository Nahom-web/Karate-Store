using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;

namespace nhH60Store.Controllers {

    [Route("Product")]
    public class ProductController : Controller {

        [Route("")]
        public async Task<IActionResult> Index() {
            try {
                Product product = new Product();
                return View(await product.GetAllProducts());
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [HttpGet, Route("Create")]
        public async Task<IActionResult> Create(int id) {
            try {
                Product product = new Product();
                ProductCategory categories = new ProductCategory();
                ViewData["ProdCatId"] = new SelectList(await categories.GetAllCategories(), "CategoryId", "ProdCat");
                return View(product);
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(Product product) {

            try {
                HttpResponseMessage response = await product.CreateProduct();
                if (response.IsSuccessStatusCode) {
                    Product allProduct = new Product();
                    return RedirectToAction("Index", "Product", await allProduct.GetAllProducts());
                } else {
                    ViewData["ErrorMessage"] = response.ReasonPhrase;
                    return View(product);
                }
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View(product);
            }
        }

        [Route("Details/{id:int}")]
        public async Task<IActionResult> Detail(int id) {
            try {
                Product prod = new Product();
                return View(await prod.FindProduct(id));
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [HttpGet, Route("UpdateStock/{id:int}")]
        public async Task<IActionResult> UpdateStock(int id) {        
            try {
                Product product = new Product();
                var result = await product.FindProduct(id);
                return View(result);
            } catch(Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }            
        }

        [HttpPost, Route("UpdateStock/{id:int}")]
        public async Task<IActionResult> UpdateStock(Product product) {

            try {
                HttpResponseMessage response = await product.UpdateStock();
                if (response.IsSuccessStatusCode) {
                    Product allProduct = new Product();
                    return RedirectToAction("Index", "Product", await allProduct.GetAllProducts());
                } else {
                    ViewData["ErrorMessage"] = response.ReasonPhrase;
                    return View(product);
                }
            } catch(Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View(product);
            }
        }

        [HttpGet, Route("UpdatePrices/{id:int}")]
        public async Task<IActionResult> UpdatePrices(int id) {
            
            try {
                Product product = new Product();
                var result = await product.FindProduct(id);
                return View(result);
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [HttpPost, Route("UpdatePrices/{id:int}")]
        public async Task<IActionResult> UpdatePrices(Product product) {

            try {
                HttpResponseMessage response = await product.UpdatePrices();
                if (response.IsSuccessStatusCode) {
                    Product allProduct = new Product();
                    return RedirectToAction("Index", "Product", await allProduct.GetAllProducts());
                } else {
                    ViewData["ErrorMessage"] = response.ReasonPhrase;
                    return View(product);
                }
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View(product);
            } 
        }

        [HttpGet, Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                Product prodCat = new Product();
                HttpResponseMessage response = await prodCat.DeleteProduct(id);
                if (response.IsSuccessStatusCode) {
                    return RedirectToAction("Index", "Product", await prodCat.GetAllProducts());
                } else {
                    ViewData["ErrorMessage"] = response.ReasonPhrase;
                    return View();
                }
            } catch(Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [Route("ProductsByCategory")]
        public async Task<IActionResult> ProductsByCategory() {
            try {
                Product productsWithCategories = new Product();
                return View(await productsWithCategories.GetAllProductsWithCategories());
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }
        }

    }
}