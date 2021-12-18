using nhH60Services.Models;

namespace nhH60Services.Dtos {
    public class ProductDTO {

        public int ProductId { get; set; }

        public string Description { get; set; }

        public string Manufacturer { get; set; }

        public decimal? SellPrice { get; set; }

        public int? Stock { get; set; }

        public ProductDTO(Product P) {
            ProductId = P.ProductId;
            Description = P.Description;
            SellPrice = P.SellPrice;
            Stock = P.Stock;
            Manufacturer = P.Manufacturer;
        }

    }
}
