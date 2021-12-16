using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Customer.Dtos;
using System.Runtime.Serialization;

namespace nhH60Customer.Dtos {

    [DataContract(Name = "orderItem")]

    public class OrderItemDTO {

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

        [DataMember(Name = "total")]
        public decimal Total { get; set; }

        [DataMember(Name = "order")]
        public virtual OrderDTO Order { get; set; }

        [DataMember(Name = "product")]
        public virtual ProductDTO Product { get; set; }

    }
}
