using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace nhH60Store.Models {

    [DataContract(Name = "cartItems")]

    public class CartItem {

        [DataMember(Name = "cartItemId")]
        [Key]
        public int CartItemId { get; set; }

        [DataMember(Name = "cartId")]
        [ForeignKey("ShoppingCart")]
        public int CartId { get; set; }

        [DataMember(Name = "productId")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "price")]
        [Column(TypeName = "numeric(8,2)")]
        public decimal? Price { get; set; }

        [DataMember(Name = "product")]
        public virtual Product Product { get; set; }

        [DataMember(Name = "cart")]
        public virtual ShoppingCart Cart { get; set; }
    }
}
