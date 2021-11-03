using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace nhH60Store.Models {

    [DataContract(Name = "Customer")]

    public class Customer {

        [NotMapped]
        private readonly H60AssignmentDB_nhContext _context;


        public Customer() {
            _context = new H60AssignmentDB_nhContext();
        }



        [NotMapped]
        private const string CUSTOMERS_URL = "http://localhost:63164/api/Customer";


        public enum Provinces { ON, QC, NB, MB };

        [NotMapped]
        private const string PhoneNumberRegex = "^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-.]?([0-9]{4})$";

        [NotMapped]
        private const string CreditCardRegex = "^4\\d{3}(\\d{4}){3}$";

        [NotMapped]
        private const string NameRegex = "^[a-z\\sA-Z\'\" -]{1,}$";


        [DataMember(Name = "customerId")]
        [Key]
        public int CustomerId { get; set; }

        [DataMember(Name = "firstName")]
        [Display(Name = "First Name")]
        [Required]
        [StringLength(20, ErrorMessage = "Please enter first name under {1} characters.")]
        [RegularExpression(NameRegex, ErrorMessage = "First name can only contain letter, apostrophe's, dashes, and spaces only.")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        [Display(Name = "Last Name")]
        [Required]
        [StringLength(30, ErrorMessage = "Please enter last name under {1} characters.")]
        [RegularExpression(NameRegex, ErrorMessage = "Last name can only contain letter, apostrophe's, dashes, and spaces only.")]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "Please enter email under {1} characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [DataMember(Name = "phoneNumber")]
        [Display(Name = "Phone Number")]
        //[StringLength(10)]
        [StringLength(14, MinimumLength = 10, ErrorMessage = "Please provide 10 digit phone number.")]
        [RegularExpression(PhoneNumberRegex, ErrorMessage = "Please enter your phone number in the correct format (819)-274-3867")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "province")]
        [StringLength(2)]
        public string Province { get; set; }

        [DataMember(Name = "creditCard")]
        [Display(Name = "Credit Card")]
        [StringLength(16)]
        [RegularExpression(CreditCardRegex, ErrorMessage = "Please enter your credit card in the correct format 4### #### #### ####")]
        public string CreditCard { get; set; }

        [DataMember(Name = "orders")]
        public virtual ICollection<Order> Orders { get; set; }

        [DataMember(Name = "shoppingCart")]
        public virtual ShoppingCart Cart { get; set; }

        public string StripWhiteSpacesOrCharacters(string inp) {
            string str = "";

            for (int i = 0; i < inp.Length; i++) {
                int x;
                bool result = int.TryParse($"{inp[i]}", out x);
                if (result) {
                    str += inp[i];
                }
            }
            return str;
        }


        private void ValidatePhoneNumber() {
            if(this.PhoneNumber != null) {
                this.PhoneNumber = this.StripWhiteSpacesOrCharacters(this.PhoneNumber);
            }
        }

        private void ValidateCreditCard() {
            if (this.CreditCard != null) {
                this.CreditCard = this.StripWhiteSpacesOrCharacters(this.CreditCard);
            }
        }

        public async Task<List<Customer>> GetAllCustomers() {
            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(CUSTOMERS_URL);

            var Serializer = new DataContractJsonSerializer(typeof(List<Customer>));

            List<Customer> customer = Serializer.ReadObject(await StreamTask) as List<Customer>;

            return customer;
        }

        public async Task<Customer> FindCustomer(int id) {
            HttpClient Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository");

            string TaskString = CUSTOMERS_URL + "/" + id.ToString();

            var StreamTask = Client.GetStreamAsync(TaskString);

            var Serializer = new DataContractJsonSerializer(typeof(Customer));

            Customer customer = Serializer.ReadObject(await StreamTask) as Customer;

            return customer;

        }


        public async Task<HttpResponseMessage> Create() {

            ValidatePhoneNumber();

            ValidateCreditCard();

            string JsonString = JsonSerializer.Serialize<Customer>(this);

            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PostAsync(CUSTOMERS_URL, HttpContext);

            return Response;
        }

        public async Task<HttpResponseMessage> Update() {

            ValidatePhoneNumber();

            ValidateCreditCard();

            string JsonString = JsonSerializer.Serialize<Customer>(this);

            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PutAsync(CUSTOMERS_URL + "/" + this.CustomerId.ToString(), HttpContext);

            return Response;
        }

        public async Task<HttpResponseMessage> Delete(int id) {
            HttpClient Client = new();

            HttpResponseMessage Response = await Client.DeleteAsync(CUSTOMERS_URL + "/" + id.ToString());

            return Response;
        }
    }
}
