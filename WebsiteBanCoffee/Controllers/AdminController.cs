using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanCoffee.Models;

namespace WebsiteBanCoffee.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SideNavBar()
        {
            return PartialView();
        }

        public ActionResult ListProduct(string Cate)
        {
            var cate = _context.Categories.ToList();
            var listProduct = _context.Products.ToList();
            ViewBag.Cate = cate;
            return View(Product.GetAllProduct());
        }

        public ActionResult CreateProduct()
        {
            ViewBag.cateID = new SelectList(_context.Categories, "Id", "name");
            return View();
        }
        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.cateID = new SelectList(_context.Categories, "Id", "name", product.cateID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "Id,name,cateID,sellPrice,costPrice,description,image,isDisplay")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ListProduct", "Admin");
            }
            ViewBag.cateID = new SelectList(_context.Categories, "Id", "name", product.cateID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "Id,name,cateID,sellPrice,costPrice,description,image")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("ListProduct", "Admin");
            }

            ViewBag.cateID = new SelectList(_context.Categories, "Id", "name", product.cateID);
            return View(product);
        }

        public ActionResult ListOrder()
        {
            var listOrder = _context.Orders.Include("ApplicationUser").ToList();
            ViewBag.OrderDetails = _context.Details.Include("Product").ToList();
            return View(listOrder);
        }

        public ActionResult ListUser()
        {
            var listUser = _context.Users.ToList();
            return View(listUser);
        }

        public ActionResult ListCategory()
        {
            var listCate = _context.Categories.ToList();
            return View(listCate);
        }

        public ActionResult CategoryDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory([Bind(Include = "Id,name,isLarge,isMedium,isSmall,bonusPrice")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("ListCategory", "Admin");
            }

            return View(category);
        }

        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "Id,name,isLarge,isMedium,isSmall,bonusPrice,isDisplay")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ListCategory", "Admin");
            }
            return View(category);
        }
    }
}