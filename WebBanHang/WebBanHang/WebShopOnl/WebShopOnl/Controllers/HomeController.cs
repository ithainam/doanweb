using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopOnl.Common;
using WebShopOnl.Models;

namespace WebShopOnl.Controllers
{
    public class HomeController : Controller
    {
        WebShopOnlDbContext db = new WebShopOnlDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var productDao = new ProductDao();
            ViewBag.NewsProducts = productDao.ListNewProduct(10);
            ViewBag.HotProducts = productDao.ListHotProduct();

            //IEnumerable<Product> dsSp = db.Products.ToList().OrderByDescending(x => x.ID).Take(10);            
            //return View(dsSp);

            return View();
        }

        
        // GET: Cart
        public ActionResult HeaderCart()
        {
            var cart = Session[ComonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        //public ActionResult ProductHot()
        //{
        //    IEnumerable<Product> ds = db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.ID).ToList();

        //    return PartialView(ds);
        //}
    }
}