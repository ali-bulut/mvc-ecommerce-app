using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.MvcWebUI.Entity
{
    public class Category
    {
        public int Id { get; set; }

        //DATA ANNOTATIONS
        //Buraya Kategori Adı yazarsan vs'nin kendi oluşturduğu sayfalarda Name yerine Kategori Adı gösterilir
        [DisplayName("Category Name")]
        //20 harften uzun kategori adı kabul edilmiyor
        [StringLength(maximumLength:20, ErrorMessage = "You can enter a maximum of 20 characters")]
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}