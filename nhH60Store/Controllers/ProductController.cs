using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace nhH60Store.Controllers {

    [Route("Product")]
    public class ProductController : Controller {

        [Route("")]
        public async Task<IActionResult> Index() {
            Product product = new Product();
            return View(await product.GetAllProducts());
        }

        [HttpGet, Route("Create")]
        public async Task<IActionResult> Create(int id) {
            try {
                Product product = new Product();
                ProductCategory categories = new ProductCategory();
                ViewData["ProdCatId"] = new SelectList(await categories.GetAllCategories(), "CategoryId", "ProdCat");
                return View(product);
            } catch (Exception e) {
                return View("Error");
            }
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(Product product) {
            try {
                product.CreateProduct();
                Product allProduct = new Product();
                await allProduct.GetAllProducts();
                return RedirectToAction("Index", "Product");
            } catch (Exception e) {
                return View("Error");
            }
        }

        [Route("Details/{id:int}")]
        public async Task<IActionResult> Detail(int id) {
            try {
                Product prod = new Product();
                return View(await prod.FindProduct(id));
            } catch (Exception e) {
                return View("Error");
            }
        }

        [HttpGet, Route("UpdateStock/{id:int}")]
        public async Task<IActionResult> UpdateStock(int id) {

            Product product = new Product();

            try {
                var result = await product.FindProduct(id);
                return View(result);
            } catch(Exception e) {
                return View("Error");
            }            
        }

        [HttpPost, Route("UpdateStock/{id:int}")]
        public async Task<IActionResult> UpdateStock(Product product) {

            try {
                product.UpdateStock();
                Product allProduct = new Product();
                await allProduct.GetAllProducts();
                return RedirectToAction("Index", "Product");
            } catch(Exception e) {
                return View("Error");
            }

        }

        [HttpGet, Route("UpdatePrices/{id:int}")]
        public async Task<IActionResult> UpdatePrices(int id) {

            Product product = new Product();

            try {
                var result = await product.FindProduct(id);
                return View(result);
            } catch (Exception e) {
                return View("Error");
            }
        }

        [HttpPost, Route("UpdateStock/{id:int}")]
        public async Task<IActionResult> UpdatePrices(Product product) {
            try {
                product.UpdatePrices();
                Product allProduct = new Product();
                await allProduct.GetAllProducts();
                return RedirectToAction("Index", "Product");
            } catch (Exception e) {
                return View("Error");
            }

        }

        [HttpGet, Route("Delete/{id:int}")]
        public IActionResult Delete(int id) {
            try {
                Product prodCat = new Product();
                prodCat.DeleteProduct(id);
                return RedirectToAction("Index", "Product");
            } catch(Exception e) {
                return View("Error");
            }

        }

        [Route("ProductsByCategory")]
        public async Task<IActionResult> ProductsByCategory() {
            try {
                Product productsWithCategories = new Product();
                return View(await productsWithCategories.GetAllProductsWithCategories());
            } catch (Exception e) {
                return View("Error");
            }
        }
    }
}