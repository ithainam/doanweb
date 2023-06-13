using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShopOnl.Controllers
{
    
    public class ProductController : Controller
    {
        WebShopOnlDbContext db = new WebShopOnlDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public ActionResult ProductByTheme (long CategoryID = 1)
        {
            List<Product> lstSp = db.Products.Where(x => x.CategoryID == CategoryID).OrderByDescending(x => x.ID).ToList();
            ViewBag.Category = db.ProductCategories.Find(CategoryID);

            //var lstSp = new ProductDao().ListProduct(CategoryID);
            return View(lstSp);
        }

        //chi tiet san pham

        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(id);
            return View(product);
        }

        //Tim kiem Jjson

        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        //tim kiem

        public ActionResult Search(string keyword, int page = 1, int pageSize = 2)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;


            return View(model);
        }
    }
}