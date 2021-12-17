using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using nhH60Services.Dtos;


namespace nhH60Services.Models {
    public partial class OrderItem {

        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public OrderItem() {
            _context = new H60Assignment2DB_nhContext();
        }

        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public OrderItemDTO ToSingleDTO(OrderItem order) {
            return new OrderItemDTO(order);
        }

        public List<OrderItemDTO> ToDTO(List<OrderItem> orders) {
            List<OrderItemDTO> oDTO = new();

            foreach (var o in orders) {
                oDTO.Add(new OrderItemDTO(o));
            }

            return oDTO;
        }

        public async Task<List<OrderItem>> GetAllOrdersItems() {
            return await _context.OrderItems
                            .Include(o => o.Order)
                            .ThenInclude(c => c.Customer)
                            .Include(p => p.Product)
                            .ToListAsync();
        }

        public async Task<OrderItem> FindOrderItemById(int id) {
            return await _context.OrderItems
                             .Where(x => x.OrderItemId == id)
                             .Include(o => o.Order)
                             .ThenInclude(c => c.Customer)
                             .Include(p => p.Product)
                             .FirstAsync();
        }

        public async Task Create() {

            Order orderObj = new Order();

            var order = await orderObj.FindOrderById(this.OrderId);

            if(order.Total == null) {
                order.Total = this.CalculateTotalOrderItemPrice();
            } else {
                order.Total += this.CalculateTotalOrderItemPrice();
            }            

            _context.OrderItems.Add(this);

            await _context.SaveChangesAsync();

            await order.Update();
        }

        public async Task Update() {
            H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();
            _db.Entry(this).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete() {
            _context.OrderItems.Remove(this);
            await _context.SaveChangesAsync();
        }

        public decimal CalculateTotalOrderItemPrice() {
            return (decimal)(this.Price * this.Quantity);
        }
    }
}
