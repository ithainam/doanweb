using WebsiteBanCoffee.Models;
using WebsiteBanCoffee.Models.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using WebsiteBanCoffee.ViewModels;
using WebsiteBanCoffee;

namespace WebsiteBanCoffee.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public ShoppingCartVM ShoppingCartVM = new ShoppingCartVM();

        public int OrderTotal { get; set; }


        public CartController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        [Authorize]
        // GET: Cart
        public ActionResult Index()
        {
            ApplicationUser loginUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            string userId = loginUser.Id;

            ApplicationDbContext context = new ApplicationDbContext();

            ShoppingCartVM = new ShoppingCartVM();
            ShoppingCartVM = new ShoppingCartVM
            {
                ListCart = context.ShoppingCarts.Include(u => u.Product).Where(u => u.UserId == userId && u.Product.isDisplay != false),
                Order = new Order()
            };

            if (ShoppingCartVM.ListCart.Count() == 0)
            {
                ViewBag.ListCount = 0;
                return View();
            }

            foreach (var cart in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.Order.OrderTotal += cart.Price;
            }

            ViewBag.ListCount = ShoppingCartVM.ListCart.Count();

            Session["CountCart"] = context.ShoppingCarts.Count();
            return View(ShoppingCartVM);
        }

        [HttpGet]
        [Authorize]
        // POST: /Cart/Summary
        public ActionResult Summary()
        {
            ApplicationUser loginUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            string userId = loginUser.Id;

            ApplicationDbContext context = new ApplicationDbContext();

            ShoppingCartVM = new ShoppingCartVM();
            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = context.ShoppingCarts.Where(u => u.UserId == userId && u.Product.isDisplay != false).Include("Product"),
                Order = new Order()
            };

            ShoppingCartVM.Order.ApplicationUser = context.Users.FirstOrDefault(u => u.Id == userId);

            ShoppingCartVM.Order.Name = ShoppingCartVM.Order.ApplicationUser.FullName;
            ShoppingCartVM.Order.PhoneNumber = ShoppingCartVM.Order.ApplicationUser.PhoneNumber;
            ShoppingCartVM.Order.StreetAddress = ShoppingCartVM.Order.ApplicationUser.Address;
            //ShoppingCartVM.Order.City = ShoppingCartVM.Order.ApplicationUser.City;
            //ShoppingCartVM.Order.State = ShoppingCartVM.Order.ApplicationUser.State;

            foreach (var cart in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.Order.OrderTotal += cart.Price;
            }

            if (ShoppingCartVM.Order.OrderTotal == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [Authorize]
        // POST: /Cart/Summary?ShoppingCartVM=
        public ActionResult Summary(ShoppingCartVM ShoppingCartVM)
        {
            ApplicationUser loginUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            string userId = loginUser.Id;

            ApplicationDbContext context = new ApplicationDbContext();


            ShoppingCartVM.ListCart = context.ShoppingCarts.Where(u => u.UserId == userId && u.Product.isDisplay != false).Include(c => c.Product);

            if (ShoppingCartVM.Order.Name == null || ShoppingCartVM.Order.PhoneNumber == null || ShoppingCartVM.Order.StreetAddress == null /*|| ShoppingCartVM.Order.State == null*/)
            {
                return RedirectToAction("Summary", "Cart");
            }

            ShoppingCartVM.Order.PaymentStatus = "Pending";
            ShoppingCartVM.Order.OrderStatus = "Pending";
            ShoppingCartVM.Order.OrderDate = DateTime.Now;
            ShoppingCartVM.Order.UserId = userId;

            foreach (var cart in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.Order.OrderTotal += cart.Price;
            }

            context.Orders.Add(ShoppingCartVM.Order);
            context.SaveChanges();

            foreach (var cart in ShoppingCartVM.ListCart.ToList())
            {
                OrderDetails orderHeaderDetails = new OrderDetails()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartVM.Order.Id,
                    Count = cart.Count,
                    size = cart.SizeName,
                };

                context.Details.AddOrUpdate(orderHeaderDetails);
                context.SaveChanges();
            }

            context.ShoppingCarts.RemoveRange(ShoppingCartVM.ListCart.Where(u => u.UserId == userId));
            context.SaveChanges();

            Session["CountCart"] = context.ShoppingCarts.Count();

            return RedirectToAction("OrderConfirmation", "Cart");
        }

        [HttpGet]
        [Authorize]
        // GET: /Cart/OrderConfirmation
        public ActionResult OrderConfirmation()
        {
            ApplicationUser loginUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            string userId = loginUser.Id;

            ApplicationDbContext context = new ApplicationDbContext();

            ViewBag.NumberOrder = context.Orders.Where(u => u.UserId == userId).Count();

            Session["CountCart"] = context.ShoppingCarts.Count();
            return View();
        }



        public ActionResult Plus(int cartId)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);

            cart.IncrementCount(cart, 1);

            cart.Price += cart.Product.sellPrice + cart.SizePrice;

            _context.SaveChanges();

            Session["CountCart"] = _context.ShoppingCarts.Count();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Minus(int cartId)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);

            if (cart.Count <= 1)
            {
                _context.ShoppingCarts.Remove(cart);
            }
            else
            {
                cart.Price -= (cart.Product.sellPrice + cart.SizePrice);
                cart.DecrementCount(cart, 1);
            }

            _context.SaveChanges();

            Session["CountCart"] = _context.ShoppingCarts.Count();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Remove(int cartId)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);

            _context.ShoppingCarts.Remove(cart);

            _context.SaveChanges();

            Session["CountCart"] = _context.ShoppingCarts.Count();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult ClearShoppingCart()
        {
            ApplicationUser loginUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            string userId = loginUser.Id;

            ShoppingCartVM.ListCart = _context.ShoppingCarts.Where(u => u.UserId == userId).Include(c => c.Product);

            _context.ShoppingCarts.RemoveRange(ShoppingCartVM.ListCart.Where(u => u.UserId == userId));
            _context.SaveChanges();

            Session["CountCart"] = _context.ShoppingCarts.Count();

            return RedirectToAction(nameof(Index));
        }

    }
}