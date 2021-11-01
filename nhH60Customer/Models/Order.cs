using System;
using System.Collections.Generic;

#nullable disable

namespace nhH60Customer.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFulfilled { get; set; }
        public decimal? Total { get; set; }
        public decimal? Taxes { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
