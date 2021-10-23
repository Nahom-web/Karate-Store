using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace nhH60Store.Models {
    public class Customer {

        [Key]
        public int CustomerId { get; set; }

        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [StringLength(2)]
        public string Province { get; set; }

        [StringLength(16)]
        public string CreditCard { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ShoppingCart Cart { get; set; }
    }
}
