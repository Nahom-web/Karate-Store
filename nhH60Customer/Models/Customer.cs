using System;
using System.Collections.Generic;


namespace nhH60Customer.Models {
    public partial class Customer {
        public Customer() {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string CreditCard { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
