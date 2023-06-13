using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using web3.Models;

namespace web3.Controllers
{
    public class NguoiDungController : Controller
    {
        QLSEntities db = new QLSEntities();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
            }

            return View("Index", "Home");
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string stk = f["txtTaiKhoan"].ToString();
            string smk = f.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TK == stk && n.MK == smk);
            if (kh != null)
            {
                Session["TaiKhoan"] = kh;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ThongBao = "sai hoặc tài khoản không tồn tại";
            return View();
        }
    }
}