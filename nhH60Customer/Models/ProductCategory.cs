using System;
using System.Collections.Generic;

#nullable disable

namespace nhH60Customer.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string ProdCat { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
