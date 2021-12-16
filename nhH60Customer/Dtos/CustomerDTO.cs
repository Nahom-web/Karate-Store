using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;
using System.Runtime.Serialization;

namespace nhH60Customer.Dtos {

    [DataContract(Name = "customer")]

    public class CustomerDTO {

        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "province")]
        public string Province { get; set; }

        [DataMember(Name = "creditCard")]
        public string CreditCard { get; set; }


    }
}
