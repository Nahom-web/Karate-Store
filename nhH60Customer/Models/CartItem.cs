using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace nhH60Customer.Models {

    [DataContract(Name = "CartItem")]

    public partial class CartItem {

        [NotMapped]
        private const string API_URL = "http://localhost:63164/api/CartItems";

        [DataMember(Name = "cartItemId")]
        public int CartItemId { get; set; }

        [DataMember(Name = "cartId")]
        public int CartId { get; set; }

        [DataMember(Name = "productId")]
        public int ProductId { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "price")]
        public decimal? Price { get; set; }

        [DataMember(Name = "cart")]

        public virtual ShoppingCart Cart { get; set; }

        [DataMember(Name = "product")]
        public virtual Product Product { get; set; }
    }
}
