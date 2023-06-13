using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using web3.Models;

namespace web3.Controllers
{
    public class GioHangController : Controller
    {
        QLSEntities db = new QLSEntities();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> LGH = Session["GioHang"] as List<GioHang>;
            if (LGH == null)
            {
                LGH = new List<GioHang>();
                Session["GioHang"] = LGH;
            }
            return LGH;

        }
        public ActionResult ThemGioHang(int iMaSach,int iCsequence ,string strURL)
        {
            Sach s = db.Saches.SingleOrDefault(n => n.IDSach == iMaSach);
            Chapter c = db.Chapters.SingleOrDefault(n=>n.CSequence== iCsequence);
            if (s == null&&c==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> LGH = LayGioHang();
            GioHang gh = LGH.Find(n => n.iMaSach == iMaSach && n.iCsequence == iCsequence);
            
            if (gh == null)
            {
                gh = new GioHang(iMaSach,iCsequence);
                LGH.Add(gh);
                return Redirect(strURL);
            }
            else
            {
                gh.iSoLuong++;
                return Redirect(strURL);
            }
        }

        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            Sach s = db.Saches.SingleOrDefault(n => n.IDSach == iMaSP);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> LGH = LayGioHang();
            GioHang gh = LGH.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (gh != null)
            {
                gh.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int iMaSP)
        {
            Sach s = db.Saches.SingleOrDefault(n => n.IDSach == iMaSP);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> LGH = LayGioHang();
            GioHang gh = LGH.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (gh != null)
            {
                LGH.RemoveAll(n => n.iMaSach == gh.iMaSach);

            }
            if (LGH.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> LGH = LayGioHang();
            return View(LGH);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> LGH = Session["GioHang"] as List<GioHang>;
            if (LGH != null)
            {
                iTongSoLuong = LGH.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> LGH = Session["GioHang"] as List<GioHang>;
            if (LGH != null)
            {
                dTongTien = LGH.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> LGH = LayGioHang();
            return View(LGH);
        }
        public ActionResult Dathang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            DH ddh = new DH();

            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.IDKH = kh.IDKH;
            
            ddh.NgayDat = DateTime.Now;
            db.DHs.Add(ddh);
            db.SaveChanges();
            foreach (var item in gh)
            {
                CTDH ctdh = new CTDH();
                ctdh.IDDH = ddh.IDDH;
                ctdh.IDSach = item.iMaSach;
                ctdh.Soluong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDonGia;
                db.CTDHs.Add(ctdh);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}