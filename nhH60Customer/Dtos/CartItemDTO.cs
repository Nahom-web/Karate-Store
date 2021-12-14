using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;
using System.Runtime.Serialization;

namespace nhH60Customer.Dtos {

    [DataContract(Name = "CartItem")]

    public class CartItemDTO {

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

        [DataMember(Name = "total")]
        public decimal? Total { get; set; }

        [DataMember(Name = "cart")]
        public virtual ShoppingCartDTO Cart { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

    }
}
