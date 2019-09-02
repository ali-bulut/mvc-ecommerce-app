using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ECommerce.MvcWebUI.Entity
{
    public class Category
    {
        public int Id { get; set; }

        //Buraya Kategori Adı yazarsan vs'nin kendi oluşturduğu sayfalarda Name yerine Kategori Adı gösterilir
        //[DisplayName("Kategori Adı")]
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}