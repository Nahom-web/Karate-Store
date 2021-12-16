using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;
using System.Runtime.Serialization;

namespace nhH60Customer.Dtos {

    [DataContract(Name = "customerId")]

    public class CustomerCreditCardCheckDTO {

        [DataMember(Name = "cartItemId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "creditCard")]
        public string CreditCard { get; set; }

        [DataMember(Name = "creditCardResult")]
        public int CreditCardResult { get; set; }

    }
}
