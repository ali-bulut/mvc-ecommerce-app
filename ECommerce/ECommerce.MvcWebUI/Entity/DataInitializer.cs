using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ECommerce.MvcWebUI.Entity
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            List<Category> categories = new List<Category>()
            {
                new Category(){Name="Camera", Description="Camera Products"},
                new Category(){Name="Computer", Description="Computer Products"},
                new Category(){Name="Phone", Description="Phone Products"},
                new Category(){Name="Electronics", Description="Electronic Products"}
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            context.SaveChanges();


            List<Product> products = new List<Product>()
            {
                new Product(){Name="Apple iPhone 7 32 GB",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=600,IsApproved=true,Stock=100,CategoryId=3,IsHome=true},
                new Product(){Name="Apple iPhone 8 32 GB",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=800,IsApproved=true,Stock=200,CategoryId=3,IsHome=true},
                new Product(){Name="Apple iPhone X 128 GB",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=1000,IsApproved=true,Stock=120,CategoryId=3,IsHome=true},
                new Product(){Name="Apple iPhone XS 256 GB",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=1200,IsApproved=true,Stock=150,CategoryId=3,IsHome=true},
                new Product(){Name="Asus Zenbook Ux333fn",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=800,IsApproved=true,Stock=130,CategoryId=2,IsHome=true},
                new Product(){Name="Asus Zenbook Ux433fn",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=850,IsApproved=true,Stock=140,CategoryId=2,IsHome=true},
                new Product(){Name="Asus Zenbook Ux331fn",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=700,IsApproved=true,Stock=160,CategoryId=2,IsHome=true},
                new Product(){Name="Canon Eos 1200D",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=400,IsApproved=true,Stock=50,CategoryId=1,IsHome=true},
                new Product(){Name="Canon Eos 100D",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=200,IsApproved=true,Stock=60,CategoryId=1,IsHome=false},
                new Product(){Name="Canon Eos 700D",Description="Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.",Price=300,IsApproved=true,Stock=70,CategoryId=1,IsHome=true}
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();

            base.Seed(context); 
        }
    }
}