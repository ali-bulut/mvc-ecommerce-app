using ECommerce.MvcWebUI.Entity;
using ECommerce.MvcWebUI.Identity;
using ECommerce.MvcWebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        [Authorize]
        public ActionResult Index()
        {
            var orders = db.Orders.Where(i => i.Username == User.Identity.Name).Select(i => new UserOrderModel()
            {

                Id=i.Id,
                OrderNumber=i.OrderNumber,
                OrdeDate=i.OrderDate,
                OrderState=i.OrderState,
                Total=i.Total
            }).OrderByDescending(i=>i.OrdeDate).ToList();
            return View(orders);
        }

        [Authorize]
        public ActionResult Details(int id)
        {

            var entity = db.Orders.Where(i => i.Id == id).FirstOrDefault();
            //.Select(i => new OrderDetailsModel()
            //{
            //    OrderId = i.Id,
            //    OrderNumber = i.OrderNumber,
            //    Total = i.Total,
            //    OrderDate = i.OrderDate,
            //    OrderState = i.OrderState,
            //    AddressTitle = i.AddressTitle,
            //    Address = i.Address,
            //    City = i.City,
            //    Town = i.Town,
            //    District = i.District,
            //    PostCode = i.PostCode,
            //OrderLines = i.OrderLines.Select(x=> new OrderLineModel() {

            //    ProductId = x.ProductId,
            //    ProductName = x.Product.Name,
            //    Image = x.Product.Image,
            //    Quantity = x.Quantity,
            //    Price = x.Price

            //}).ToList()
            //}).FirstOrDefault();

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
                OrderLines = entity.OrderLines.Select(x => new OrderLineModel()
                {

                    ProductId = x.ProductId,
                    ProductName = x.Product.Name.Length>50?x.Product.Name.Substring(0,47)+"...": x.Product.Name,
                    Image = x.Product.Image == null ? "NoImage.jpg" : x.Product.Image,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };
            

            return View(model);
        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;

                IdentityResult result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    //bu hatalar cshtmlde validationsummary kısmında çıkar
                    ModelState.AddModelError("RegisterUserError", "Error creating user");
                }
            }
            return View(model);
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.Username, model.Password);

                if (user!=null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    var identityClaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();

                    //kalıcı bir cookie olup olmadığını beni hatırla kutucuğundaki işarete göre belirleyecek
                    authProperties.IsPersistent = model.RememberMe;

                    authManager.SignIn(authProperties, identityClaims);

                   // TempData["role"] = user.Roles.Where(i => i.RoleId == "41d33f13-e4f6-490a-bf89-97f68c594fbb").FirstOrDefault();

                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //bu hatalar cshtmlde validationsummary kısmında çıkar
                    ModelState.AddModelError("LoginUserError", "No such user");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            //oluşturduğumuz cookieyi sistemden siliyoruz
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}