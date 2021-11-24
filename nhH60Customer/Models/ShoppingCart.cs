using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

#nullable disable

namespace nhH60Customer.Models {
    [DataContract(Name = "ShoppingCart")]
    public partial class ShoppingCart {

        [NotMapped]
        private const string API_URL = "http://localhost:63164/api/Carts";

        public ShoppingCart() {
            CartItems = new HashSet<CartItem>();
        }

        [DataMember(Name = "cartId")]
        public int CartId { get; set; }

        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "dateCreated")]
        public DateTime DateCreated { get; set; }

        [DataMember(Name = "customer")]
        public virtual Customer Customer { get; set; }

        [DataMember(Name = "cartItems")]
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
