﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.MvcWebUI.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }

        public EnumOrderState OrderState { get; set; }

        public string Username { get; set; }

        public string AddressTitle { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Town { get; set; }

        public string District { get; set; }

        public string PostCode { get; set; }


        public virtual List<OrderLine> OrderLines { get; set; }
    }
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        //Lazy loading kavramını etkin hale getirmek için virtual diyoruz
        public virtual Order Order { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}