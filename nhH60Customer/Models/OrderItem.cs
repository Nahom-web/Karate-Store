using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

#nullable disable

namespace nhH60Customer.Models {

    [DataContract(Name = "OrderItem")]
    public partial class OrderItem {

        [NotMapped]
        private const string API_URL = "http://localhost:63164/api/OrderItems";

        [DataMember(Name = "orderItemId")]
        public int OrderItemId { get; set; }

        [DataMember(Name = "orderId")]
        public int OrderId { get; set; }

        [DataMember(Name = "productId")]
        public int ProductId { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "price")]
        public decimal? Price { get; set; }

        [DataMember(Name = "order")]
        public virtual Order Order { get; set; }

        [DataMember(Name = "product")]
        public virtual Product Product { get; set; }
    }
}
