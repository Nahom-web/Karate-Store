﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using nhH60Customer.Dtos;

namespace nhH60Customer.Models {

    [DataContract(Name = "Product")]

    public partial class Product {

        [NotMapped]
        private const string API_URL = "http://localhost:63164/api/Products";


        [DataMember(Name = "productId")]
        public int ProductId { get; set; }

        [DataMember(Name = "prodCatId")]
        [Display(Name = "Category")]
        public int ProdCatId { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "manufacturer")]
        public string Manufacturer { get; set; }

        [DataMember(Name = "stock")]
        public int? Stock { get; set; }

        [DataMember(Name = "buyPrice")]
        [Display(Name = "Buy Price")]
        public decimal? BuyPrice { get; set; }

        [DataMember(Name = "sellPrice")]
        [Display(Name = "Sell Price")]
        public decimal? SellPrice { get; set; }

        [DataMember(Name = "prodCat")]
        public virtual ProductCategory ProdCat { get; set; }

        [DataMember(Name = "cartItems")]
        public virtual ICollection<CartItem> CartItems { get; set; }

        [DataMember(Name = "orderItems")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public async Task<List<ProductDTO>> GetAllProducts() {
            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(API_URL + "/CustomerProducts");

            var Serializer = new DataContractJsonSerializer(typeof(List<ProductDTO>));

            List<ProductDTO> Products = Serializer.ReadObject(await StreamTask) as List<ProductDTO>;

            return Products;

        }

        public async Task<List<ProductDTO>> FindProduct(string ProductName) {
            HttpClient Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository");

            string TaskString = API_URL + "/CustomerProducts?ProductName=" + ProductName;

            var StreamTask = Client.GetStreamAsync(TaskString);

            var Serializer = new DataContractJsonSerializer(typeof(List<ProductDTO>));

            List<ProductDTO> product = Serializer.ReadObject(await StreamTask) as List<ProductDTO>;

            return product;

        }

    }
}
