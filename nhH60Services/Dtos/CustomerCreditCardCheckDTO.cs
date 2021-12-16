using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Services.Models;

namespace nhH60Services.Dtos {
    public class CustomerCreditCardCheckDTO {

        public int CustomerId { get; set; }
        public string CreditCard { get; set; }
        public int CreditCardResult { get; set; }

        public CustomerCreditCardCheckDTO(Customer c, int result) {
            CustomerId = c.CustomerId;
            CreditCard = c.CreditCard;
            CreditCardResult = result;
        }
    }
}
