using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using web3.Models;

namespace web3.Controllers
{
    public class NXBController : Controller
    {
        // GET: NXB
        QLSEntities db = new QLSEntities();
        public PartialViewResult NXBPartial()
        {
            return PartialView(db.NXBs.Take(10).OrderBy(x => x.TenNXB).ToList());
        }
        public ViewResult SachTheoNXB(int MaNXB = 0)
        {
            NXB nxb = db.NXBs.SingleOrDefault(n => n.IDNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Sach> ListS = db.Saches.Where(n => n.IDNXB == MaNXB).OrderBy(n => n.Gia).ToList();
            if (ListS.Count == 0)
            {
                ViewBag.Sach = "ko co sach";
            }
            return View(ListS);
        }
        public ViewResult DanhMucNXB()
        {
            return View(db.NXBs.ToList());
        }
    }
}