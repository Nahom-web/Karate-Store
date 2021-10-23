using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace nhH60Store.Models {
    public class CartItem {

        [Key]
        public int CartItemId { get; set; }

        [ForeignKey("ShoppingCart")]
        public int CartId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "numeric(8,2)")]
        public decimal? Price { get; set; }
        public virtual Product Product { get; set; }   
        public virtual ShoppingCart Cart { get; set; }
    }
}
