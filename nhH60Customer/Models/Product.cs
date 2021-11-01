using System;
using System.Collections.Generic;

#nullable disable

namespace nhH60Customer.Models
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public int ProdCatId { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public int? Stock { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }

        public virtual ProductCategory ProdCat { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
