using System;
using System.Collections.Generic;

#nullable disable

namespace nhH60Services.Models
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
