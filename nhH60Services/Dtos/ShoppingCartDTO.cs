using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhH60Services.Models;

namespace nhH60Services.Dtos {

    public class ShoppingCartDTO {

        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public string DateCreated { get; set; }
        public virtual ICollection<CartItemDTO> CartItems { get; set; }

        public ShoppingCartDTO(ShoppingCart cart) {
            CartId = cart.CartId;
            CustomerId = cart.CustomerId;
            DateCreated = cart.DateCreated.ToString("yyyy\\MM\\/dd");
            CartItems = ToDTO(cart.CartItems);
        }

        private ICollection<CartItemDTO> ToDTO(ICollection<CartItem> CartItemsInpt) {
            List<CartItemDTO> pDTO = new();

            foreach (var p in CartItemsInpt) {
                pDTO.Add(new CartItemDTO(p));
            }

            return pDTO;
        }
    }
}
