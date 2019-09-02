using ECommerce.MvcWebUI.Entity;
using ECommerce.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext context = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var products = context.Products.Select(i => new ProductModel()
            {
                Id = i.Id,
                Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                Price = i.Price,
                Stock = i.Stock,
                IsApproved = i.IsApproved,
                IsHome = i.IsHome,
                Image = i.Image == null ? "NoImage.jpg" : i.Image,
                CategoryId = i.CategoryId
            })
            .Where(i => i.IsHome == true && i.IsApproved == true)
            .ToList();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            return View(context.Products.Where(i => i.Id == id).FirstOrDefault());
        }

        //?(nullable) bu işaret ile illa id göndermemize gerek yok yani home/list dediğimizde de çalışır
        // home/list/1 dediğimizde de çalışır
        public ActionResult List(int? id)
        {
            var products = context.Products.Select(i => new ProductModel()
            {
                Id = i.Id,
                Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                Price = i.Price,
                Stock = i.Stock,
                IsApproved = i.IsApproved,
                IsHome = i.IsHome,
                Image = i.Image == null ? "NoImage.jpg" : i.Image,
                CategoryId = i.CategoryId
            })
            .Where(i => i.IsApproved == true)
            .AsQueryable();

            if (id != null)
            {
                products = products.Where(i => i.CategoryId == id);
            }

            return View(products.ToList());
        }

        public PartialViewResult GetCategories()
        {
            return PartialView(context.Categories.ToList());
        }
    }
}