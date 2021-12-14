using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Services.Models;

namespace nhH60Services.Dtos {
    public class CartItemDTO {

        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal Total { get; set; }
        public string Description { get; set; }


        public CartItemDTO(CartItem C) {
            CartItemId = C.CartItemId;
            CartId = C.CartId;
            ProductId = C.ProductId;
            Quantity = C.Quantity;
            Price = C.Price;
            Total = (decimal)(Quantity * Price);
            Description = C.Product.Description;
        }
    }
}
