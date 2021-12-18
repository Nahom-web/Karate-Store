using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using nhH60Services.Dtos;
using System.Text.Json.Serialization;

namespace nhH60Services.Models {
    public partial class Order {

        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public Order() {
            OrderItems = new HashSet<OrderItem>();
            _context = new H60Assignment2DB_nhContext();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        [DataType(DataType.Date)]
        //[JsonIgnore]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateFulfilled { get; set; }
        public decimal? Total { get; set; }
        public decimal? Taxes { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }


        public async Task<List<Order>> GetAllOrders() {
            var orders = await _context.Orders.Include(c => c.Customer).Include(o => o.OrderItems).ThenInclude(p => p.Product).ToListAsync();
            return orders;
        }

        public async Task<Order> FindOrderById(int id) {
            return await _context.Orders.Where(x => x.OrderId == id).Include(c => c.Customer).Include(o => o.OrderItems).ThenInclude(p => p.Product).FirstOrDefaultAsync();
        }


        public OrderDTO ToSingleDTO(Order order) {
            return new OrderDTO(order);
        }

        public List<OrderDTO> ToDTO(List<Order> orders) {
            List<OrderDTO> oDTO = new();

            foreach (var o in orders) {
                oDTO.Add(new OrderDTO(o));
            }

            return oDTO;
        }

        public async Task<List<Order>> FindOrderByDate(string date) {
            var toDate = Convert.ToDateTime(date);
            return await _context.Orders.Where(x => x.DateFulfilled == toDate).ToListAsync();
        }

        public async Task<List<Order>> FindOrdersForCustomer(int id) {
            return await _context.Orders.Where(x => x.CustomerId == id).Include(c => c.Customer).ToListAsync();
        }

        public async Task Create() {
            this.DateCreated = DateTime.Now;
            _context.Orders.Add(this);
            await _context.SaveChangesAsync();
        }

        public async Task Update() {
            H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();
            _db.Entry(this).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateFinalizedOrder(int id) {
            var order = await FindOrderById(id);

            order.DateFulfilled = this.DateFulfilled;

            order.Taxes = this.Taxes;

            await order.Update();

            //_context.Orders.Update(order);

            //await _context.SaveChangesAsync();
        }

        public async Task Delete() {
            _context.Orders.Remove(this);
            await _context.SaveChangesAsync();
        }

    }
}
