using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

#nullable disable

namespace nhH60Customer.Models {

    [DataContract(Name = "Order")]
    public partial class Order {

        [NotMapped]
        private const string API_URL = "http://localhost:63164/api/Orders";

        public Order() {
            OrderItems = new HashSet<OrderItem>();
        }

        [DataMember(Name = "orderId")]
        public int OrderId { get; set; }

        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "dateCreated")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataMember(Name = "dateFulfilled")]
        [DataType(DataType.Date)]
        public DateTime? DateFulfilled { get; set; }

        [DataMember(Name = "total")]
        public decimal? Total { get; set; }

        [DataMember(Name = "taxes")]
        public decimal? Taxes { get; set; }

        [DataMember(Name = "customer")]
        public virtual Customer Customer { get; set; }

        [DataMember(Name = "orderItems")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
