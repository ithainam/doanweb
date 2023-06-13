using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using web3.Models;

namespace web3.Controllers
{
    [Authorize(Roles ="Admin")]
    public class QLSPController : Controller
    {
        // GET: QLSP
        QLSEntities db = new QLSEntities();
        public ActionResult Index()
        {
            return View(db.Saches.ToList()); ;
        }
        [HttpGet]
        public ActionResult Create()
        {

            ViewBag.IDChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "IDChuDe", "TenChuDe");
            ViewBag.IDNXB = new SelectList(db.NXBs.ToList().OrderBy(n => n.TenNXB), "IDNXB", "TenNXB");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Sach s, HttpPostedFileBase fileUpload)
        {

            ViewBag.IDChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "IDChuDe", "TenChuDe");
            ViewBag.IDNXB = new SelectList(db.NXBs.ToList().OrderBy(n => n.TenNXB), "IDNXB", "TenNXB");
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "chọn hình đi bro";
                return View();
            }
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/images2"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                s.AnhBia = fileUpload.FileName;
                db.Saches.Add(s);
                db.SaveChanges();
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int MaSach)
        {
            Sach s = db.Saches.SingleOrDefault(n => n.IDSach == MaSach);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.IDChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "IDChuDe", "TenChuDe", s.IDChuDe);
            ViewBag.IDNXB = new SelectList(db.NXBs.ToList().OrderBy(n => n.TenNXB), "IDNXB", "TenNXB", s.IDNXB);
            return View(s);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Sach s, FormCollection f)
        {
            //Sach s1 = db.Saches.SingleOrDefault(n => n.IDSach == s.IDSach);
            //s1.Mota = s.Mota;
            //s1.Mota = f.Get("abc").ToString();
            //s1.Mota = f["abc"].ToString();
            //db.SaveChanges();

            if (ModelState.IsValid)
            {
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.IDChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "IDChuDe", "TenChuDe", s.IDChuDe);
            ViewBag.IDNXB = new SelectList(db.NXBs.ToList().OrderBy(n => n.TenNXB), "IDNXB", "TenNXB", s.IDNXB);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int MaSach)
        {
            Sach s = db.Saches.SingleOrDefault(n => n.IDSach == MaSach);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(s);

        }
        [HttpGet]
        public ActionResult Delete(int MaSach)
        {

            Sach s = db.Saches.SingleOrDefault(n => n.IDSach == MaSach);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(s);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateInput(false)]
        public ActionResult rusDELETE(int MaSach)
        {
            Sach s = db.Saches.SingleOrDefault(n => n.IDSach == MaSach);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}