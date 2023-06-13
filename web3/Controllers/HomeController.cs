using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web3.Models;
namespace web3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        QLSEntities db = new QLSEntities();
        public ActionResult Index()
        {
            //if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            //{
            //    return RedirectToAction("DangNhap", "NguoiDung");
            //}
            return View(db.Saches.ToList());
        }

    }
}