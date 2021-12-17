using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Models;
using nhH60Customer.Dtos;
using System.Runtime.Serialization;

namespace nhH60Customer.Dtos {

    [DataContract(Name = "Order")]

    public class OrderDTO {

        [DataMember(Name = "orderId")]
        public int OrderId { get; set; }

        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "dateCreated")]
        public string DateCreated { get; set; }

        [DataMember(Name = "dateFulfilled")]
        public string DateFulfilled { get; set; }

        [DataMember(Name = "total")]
        public decimal? Total { get; set; }

        [DataMember(Name = "taxes")]
        public decimal? Taxes { get; set; }

        [DataMember(Name = "customer")]
        public virtual CustomerDTO Customer { get; set; }

        [DataMember(Name = "grandTotal")]
        public decimal GrandTotal { get; set; }

        [DataMember(Name = "orderItems")]
        public virtual List<OrderItemDTO> OrderItems { get; set; }

    }
}
