using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace nhH60Store.Models {
    public class Order {

        [Key]
        public int OrderId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateFulfilled { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? Total { get; set; }

        [Column(TypeName = "numeric(8,2)")]
        public decimal? Taxes { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
