using ECommerce.MvcWebUI.Entity;
using ECommerce.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.MvcWebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Select(i => new AdminOrderModel() {

                Id = i.Id,
                OrderNumber=i.OrderNumber,
                OrdeDate=i.OrderDate,
                OrderState=i.OrderState,
                Total=i.Total,
                Count=i.OrderLines.Count

            }).OrderByDescending(i=>i.OrdeDate).ToList();
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var entity = db.Orders.Where(i => i.Id == id).FirstOrDefault();

            var model = new OrderDetailsModel()
            {
                OrderId = entity.Id,
                OrderNumber = entity.OrderNumber,
                Total = entity.Total,
                OrderDate = entity.OrderDate,
                OrderState = entity.OrderState,
                AddressTitle = entity.AddressTitle,
                Address = entity.Address,
                City = entity.City,
                Town = entity.Town,
                District = entity.District,
                PostCode = entity.PostCode,
                Username=entity.Username,
                OrderLines = entity.OrderLines.Select(x => new OrderLineModel()
                {

                    ProductId = x.ProductId,
                    ProductName = x.Product.Name.Length > 50 ? x.Product.Name.Substring(0, 47) + "..." : x.Product.Name,
                    Image = x.Product.Image == null ? "NoImage.jpg" : x.Product.Image,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };
            return View(model);

        }

        public ActionResult UpdateOrderStatus(int OrderId, EnumOrderState OrderState)
        {
            var order = db.Orders.Where(i => i.Id == OrderId).FirstOrDefault();

            if (order!=null)
            {
                order.OrderState = OrderState;
                db.SaveChanges();

                TempData["message"] = "Your information has been saved.";

                return RedirectToAction("Details", new { id = OrderId });
            }
            return RedirectToAction("Index");
        }
    }
}