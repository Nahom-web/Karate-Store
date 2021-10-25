﻿using System;
using System.Collections.Generic;

#nullable disable

namespace nhH60Services.Models
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual ShoppingCart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}