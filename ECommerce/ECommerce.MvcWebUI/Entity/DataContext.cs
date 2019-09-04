using ECommerce.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ECommerce.MvcWebUI.Entity
{
    public class DataContext:DbContext
    {
        public DataContext() : base("ecommerceConnection")
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLineModel> OrderLines { get; set; }

    }
}