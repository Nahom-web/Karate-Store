using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Store.Models;

namespace nhH60Store.Controllers {

    [Route("ProductCategory")]
    public class ProductCategoryController : Controller {

        [Route("")]
        public async Task<IActionResult> Index() {
            try {
                ProductCategory pc = new ProductCategory();
                return View(await pc.GetAllCategories());
            } catch (Exception e) {
                return View("Error");
            }

        }

        [HttpGet, Route("Create")]
        public IActionResult Create(int id) {
            try {
                ProductCategory prodCat = new ProductCategory();
                return View(prodCat);
            } catch (Exception e) {
                return View("Error");
            }

        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(ProductCategory newProdCat) {
            try {
                newProdCat.Create();
                ProductCategory allCategories = new ProductCategory();
                await allCategories.GetAllCategories();
                return RedirectToAction("Index", "ProductCategory");
            } catch (Exception e) {
                return View("Error");
            }
        }


        [HttpGet, Route("Delete/{id:int}")]
        public IActionResult Delete(int id) {
            try {
                ProductCategory prodCat = new ProductCategory();
                prodCat.Delete(id);
                return RedirectToAction("Index", "ProductCategory");
            } catch (Exception e) {
                return View("Error");
            }

        }

        [Route("ProductsForCategory/{id:int}")]
        public async Task<IActionResult> ProductsForCategory(int id) {
            try {
                ProductCategory pc = new ProductCategory();
                return View(await pc.GetProductsForCategory(id));
            } catch (Exception e) {
                return View("Error");
            }

        }

        [HttpGet, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(int id) {
            ProductCategory prodCat = new ProductCategory();
            try {
                var result = await prodCat.FindCategory(id);
                return View(result);
            } catch (Exception e) {
                return View("Error");
            }
        }

        [HttpPost, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(ProductCategory updatedProdCat) {
            try {
                updatedProdCat.Update();
                ProductCategory allCategories = new ProductCategory();
                await allCategories.GetAllCategories();
                return RedirectToAction("Index", "ProductCategory");
            } catch (Exception e) {
                return View("Error");
            }
        }
    }
}