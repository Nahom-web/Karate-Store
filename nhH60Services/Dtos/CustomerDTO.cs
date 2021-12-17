using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Services.Models;

namespace nhH60Services.Dtos {
    public class CustomerDTO {


        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string CreditCard { get; set; }

        public CustomerDTO(Customer c) {
            CustomerId = c.CustomerId;
            FirstName = c.FirstName;
            LastName = c.LastName;
            Name = FirstName + " " + LastName;
            Email = c.Email;
            PhoneNumber = "(" + c.PhoneNumber.Substring(0, 3) + ")-" + c.PhoneNumber.Substring(3, 3) + "-" + c.PhoneNumber.Substring(6, 4);
            Province = c.Province;
            CreditCard = string.Format("{0:#### #### #### ####}", Convert.ToInt64(c.CreditCard));
        }


    }
}
