using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace nhH60Customer.Dtos {

    [DataContract(Name = "Product")]
    public class ProductDTO {

        [DataMember(Name = "productId")]
        public int ProductId { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "manufacturer")]
        public string Manufacturer { get; set; }

        [DataMember(Name = "sellPrice")]
        public decimal? SellPrice { get; set; }

        [DataMember(Name = "stock")]
        public int? Stock { get; set; }

    }
}
