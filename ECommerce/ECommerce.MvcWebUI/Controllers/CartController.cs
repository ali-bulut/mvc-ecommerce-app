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

        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails model)
        {
            var cart = GetCart();
            if (cart.CartLines.Count==0)
            {
                ModelState.AddModelError("NoProductError", "There are no products in your cart.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    SaveOrder(cart, model);

                    cart.Clear();
                    return View("Completed");
                }
            }
            return View(model);
        }

        private void SaveOrder(Cart cart, ShippingDetails model)
        {
            var order = new Order();

            order.OrderNumber = "A" + (new Random()).Next(11111, 99999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.Username = User.Identity.Name;
            order.AddressTitle = model.AddressTitle;
            order.Address = model.Address;
            order.City = model.City;
            order.Town = model.Town;
            order.District = model.District;
            order.PostCode = model.PostCode;
            order.OrderState = EnumOrderState.Waiting;
            order.OrderLines = new List<OrderLine>();
            foreach (var item in cart.CartLines)
            {
                OrderLine orderLine = new OrderLine();
                orderLine.Quantity = item.Quantity;
                orderLine.Price = item.Quantity * item.Product.Price;
                orderLine.ProductId = item.Product.Id;
                
                order.OrderLines.Add(orderLine);
            }

            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}