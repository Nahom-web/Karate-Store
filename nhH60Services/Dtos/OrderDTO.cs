using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nhH60Services.Models;
using System.ServiceModel.Channels;
using System.ServiceModel;
using CalculateTaxesServiceReference;

namespace nhH60Services.Dtos {
    public class OrderDTO {

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string DateCreated { get; set; }
        public string DateFulfilled { get; set; }
        public decimal? Total { get; set; }
        public decimal? Taxes { get; set; }
        public virtual CustomerDTO Customer { get; set; }
        public decimal GrandTotal { get; set; }
        public virtual List<OrderItemDTO> OrderItems { get; set; }

        public OrderDTO(Order o) {
            OrderId = o.OrderId;
            CustomerId = o.CustomerId;
            DateCreated = o.DateCreated.ToString("yyyy\\-MM\\-dd"); ;
            DateFulfilled = Convert.ToDateTime(o.DateFulfilled).ToString("yyyy\\-MM\\-dd");
            Total = o.Total;
            if (o.Customer != null) {
                Customer = new CustomerDTO(o.Customer);
            }
            GrandTotal = Convert.ToDecimal(CalculateGrandTotalAsync().Result);
            //OrderItems = (ICollection<OrderDTO>)o.OrderItems.First().ToDTO((List<OrderItem>)o.OrderItems);
            OrderItems = CreateListOfOrderItemDTOS(o.OrderItems);
            Taxes = GrandTotal - Total;
        }

        public List<OrderItemDTO> CreateListOfOrderItemDTOS(ICollection<OrderItem> items) {
            List<OrderItemDTO> OrderItemsDTO = new();

            foreach (var o in items) {
                OrderItemsDTO.Add(new OrderItemDTO(o));
            }

            return OrderItemsDTO;
        }


        public async Task<decimal> CalculateGrandTotalAsync() {

            var tax = await CalculateTax();

            return (decimal)(Total + tax);
        }


        public async Task<decimal> CalculateTax() {

            Binding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);

            EndpointAddress endPoint = new EndpointAddress("http://csdev.cegep-heritage.qc.ca/cartService/calculateTaxes.asmx");

            var client = new CalculateTaxesSoapClient(binding, endPoint);

            var resultObject = await client.CalculateTaxAsync((double)this.Total, this.Customer.Province);

            return Convert.ToDecimal(resultObject);

        }

    }
}
