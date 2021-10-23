using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace nhH60Store.Models {
    public class ShoppingCart {

        [Key]
        public int CartId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
