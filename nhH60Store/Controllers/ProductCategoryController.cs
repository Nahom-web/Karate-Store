﻿using Microsoft.AspNetCore.Mvc;
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
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [HttpGet, Route("Create")]
        public IActionResult Create(int id) {
            try {
                ProductCategory prodCat = new ProductCategory();
                return View(prodCat);
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(ProductCategory newProdCat) {
            HttpResponseMessage response = await newProdCat.Create();
            if (response.IsSuccessStatusCode) {
                ProductCategory allCategories = new ProductCategory();
                return RedirectToAction("Index", "ProductCategory", await allCategories.GetAllCategories());
            } else {
                ViewData["ErrorMessage"] = response.ReasonPhrase;
                return View();
            }
        }

        /// <summary>
        /// Check status codes: 
        /// 404 - not found
        /// 400 - bad response
        /// 204 - good
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            ProductCategory prodCat = new ProductCategory();
            HttpResponseMessage response = await prodCat.Delete(id);
            var WholeResponseText = await response.Content.ReadAsStringAsync();
            var ErrorMessage = WholeResponseText.Substring(WholeResponseText.IndexOf(":"), WholeResponseText.IndexOf("."));
            int SCode = (int)response.StatusCode;
            if (SCode == 204) {
                return RedirectToAction("Index", "ProductCategory", await prodCat.GetAllCategories());
            } else if(SCode == 404) {
                TempData["ErrorMessage"] = response.ReasonPhrase;
                return RedirectToAction("Index");
            } else if (SCode == 400) {
                TempData["ErrorMessage"] = response.ReasonPhrase;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [Route("ProductsForCategory/{id:int}")]
        public async Task<IActionResult> ProductsForCategory(int id) {
            try {
                ProductCategory pc = new ProductCategory();
                return View(await pc.GetProductsForCategory(id));
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }

        }

        [HttpGet, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(int id) {
            ProductCategory prodCat = new ProductCategory();
            try {
                var result = await prodCat.FindCategory(id);
                return View(result);
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        [HttpPost, Route("Update/{id:int}")]
        public async Task<IActionResult> Update(ProductCategory updatedProdCat) {
            HttpResponseMessage response = await updatedProdCat.Update();
            if (response.IsSuccessStatusCode) {
                ProductCategory allCategories = new ProductCategory();
                return RedirectToAction("Index", "ProductCategory", await allCategories.GetAllCategories());
            } else {
                ViewData["ErrorMessage"] = response.ReasonPhrase;
                return View(updatedProdCat);
            }
        }

    }
}