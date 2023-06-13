using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using web3.Models;

namespace web3.Controllers
{
    public class ChuDeController : Controller
    {
        QLSEntities db = new QLSEntities();
        // GET: ChuDe
        public PartialViewResult ChuDePartial()
        {
            return PartialView(db.ChuDes.Take(5).ToList());
        }
        public ViewResult SachTheoChuDe(int MaChuDe)
        {
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.IDChuDe == MaChuDe);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Sach> ListS = db.Saches.Where(n => n.IDChuDe == MaChuDe).OrderBy(n => n.Gia).ToList();
            if (ListS.Count == 0)
            {
                ViewBag.Sach = "ko co sach";
            }
            return View(ListS);

        }
        public ViewResult DanhMucChuDe()
        {
            return View(db.ChuDes.ToList());
        }
    }
}