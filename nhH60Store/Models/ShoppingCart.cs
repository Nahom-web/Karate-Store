using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace nhH60Store.Models {

    [DataContract(Name = "shoppingCart")]

    public class ShoppingCart {

        [DataMember(Name = "cartId")]
        [Key]
        public int CartId { get; set; }

        [DataMember(Name = "customerId")]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [DataMember(Name = "dateCreated")]
        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        [DataMember(Name = "customer")]
        public virtual Customer Customer { get; set; }

        [DataMember(Name = "cartItems")]
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
