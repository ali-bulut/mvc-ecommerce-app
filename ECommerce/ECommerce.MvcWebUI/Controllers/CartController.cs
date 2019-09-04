using ECommerce.MvcWebUI.Entity;
using ECommerce.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.Where(i => i.Id == Id).FirstOrDefault();
            if (product!=null)
            {
                GetCart().AddProduct(product, 1);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.Where(i => i.Id == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            //session her kullanıcıya özel bir depo
            Cart cart = (Cart)Session["Cart"];
            if (cart==null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
        
        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }
    }
}