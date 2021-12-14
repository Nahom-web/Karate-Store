using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using nhH60Customer.Models;

namespace nhH60Customer.Dtos {

    [DataContract(Name = "ShoppingCart")]

    public class ShoppingCartDTO {

        [DataMember(Name = "cartId")]
        public int CartId { get; set; }

        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "dateCreated")]
        public string DateCreated { get; set; }

        [DataMember(Name = "cartItems")]
        public virtual ICollection<CartItemDTO> CartItems { get; set; }

    }
}
