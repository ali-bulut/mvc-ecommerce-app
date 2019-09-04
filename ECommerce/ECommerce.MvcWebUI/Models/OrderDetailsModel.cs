using ECommerce.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.MvcWebUI.Models
{
    public class OrderDetailsModel
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }

        public EnumOrderState OrderState { get; set; }
        
        public string AddressTitle { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Town { get; set; }

        public string District { get; set; }

        public string PostCode { get; set; }


        public virtual List<OrderLineModel> OrderLines { get; set; }
    }

    public class OrderLineModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}