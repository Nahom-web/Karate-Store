using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Services.Models;
using nhH60Services.Dtos;

namespace nhH60Services.Dtos {
    public class OrderItemDTO {


        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal Total { get; set; }
        public virtual ProductDTO Product { get; set; }

        public OrderItemDTO(OrderItem o) {
            OrderItemId = o.OrderItemId;
            OrderId = o.OrderId;
            ProductId = o.ProductId;
            Quantity = o.Quantity;
            Price = o.Price;
            Total = (decimal)(Quantity * Price);
            Product = new ProductDTO(o.Product);
        }
    }
}
