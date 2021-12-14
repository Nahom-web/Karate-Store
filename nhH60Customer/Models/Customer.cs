using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace nhH60Customer.Models {

    [DataContract(Name = "Customer")]

    public class Customer {

        [NotMapped]
        private const string CUSTOMERS_URL = "http://localhost:63164/api/Customers";


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

        [DataMember(Name = "orders")]
        public virtual ICollection<Order> Orders { get; set; }

        [DataMember(Name = "shoppingCart")]
        public virtual ShoppingCart Cart { get; set; }

        public async Task<Customer> FindCustomer(string CustomerEmail) {
            HttpClient Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository");

            string TaskString = CUSTOMERS_URL + "?Email=" + CustomerEmail;

            var StreamTask = Client.GetStreamAsync(TaskString);

            var Serializer = new DataContractJsonSerializer(typeof(List<Customer>));

            List<Customer> Customers = Serializer.ReadObject(await StreamTask) as List<Customer>;

            return Customers[0];

        }

    }
}
