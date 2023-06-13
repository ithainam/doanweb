using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using web3.Models;

namespace web3.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        QLSEntities db = new QLSEntities();
        public PartialViewResult SachMoiPartial()
        {
            var listsachmoi = db.Saches.Where(n => n.Moi == 1).ToList();
            return PartialView(listsachmoi);
        }
        public ViewResult XemChiTiet(int MaSach = 0)
        {
            Sach s = db.Saches.SingleOrDefault(n => n.IDSach == MaSach);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            ViewBag.TenChuDe = db.ChuDes.Single(n => n.IDChuDe == s.IDChuDe).TenChuDe;
            ViewBag.NXB = db.NXBs.Single(n => n.IDNXB == s.IDNXB).TenNXB;
            return View(s);
        }
        public ActionResult XemTruyen(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var objs = db.Chapters.ToList();
            List<Chapter> chapters = objs.Where(x => x.IDSach == id).ToList();
            if (chapters == null)
            {
                return HttpNotFound();
            }
            return View(chapters);
        }
        public ActionResult XemChapter(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Chapter objc = db.Chapters.Where(x => x.IDChapter == id).First();
            var objm = db.Chapters.Where(x => x.IDSach == objc.IDSach).ToList();
            ChapterViewModel ChapterDATA = new ChapterViewModel();
            var objt = db.Trangs.ToList();
            List<Trang> trangs = objt.Where(x => x.IDChapter == id).ToList();
            ChapterDATA.Trangs = trangs;
            ChapterDATA.Chapters = objm;
            ChapterDATA.CurrentMangaID = objc.IDSach;
            ChapterDATA.CurrentCSequence = objc.CSequence;
            ChapterDATA.TotalChapters = objm.Count();
            if (trangs == null)
            {
                return HttpNotFound();
            }
            return View(ChapterDATA);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult SuaTruyen(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var objc = db.Chapters.Where(x => x.IDSach == id).ToList();
            return View(objc);
        }
        public ActionResult SuaChapters(int id)
        {
            
            var objt = db.Trangs.Where(x => x.IDChapter == id).ToList();
            return View(objt);
        }
        [HttpGet]
        public ActionResult CreateTrang(int id)
        {
            Trang objt = new Trang
            {
                IDChapter = id
            };
            return View(objt);
        }
        [HttpPost]
        public ActionResult CreateTrang(Trang newTrang)
        {
            if (ModelState.IsValid)
            {
                
                db.Trangs.Add(newTrang);
                db.SaveChanges();
                return View("SuaChapters", Trang.GetTrangs(newTrang.IDChapter));
            }
            else
            {
                return View("CreateTrang", newTrang);
            }
        }
        public ActionResult EditTrang(int id)
        {
            
            var find = db.Trangs.Where(x => x.IDTrang == id).First();
            if (find == null)
            {
                return HttpNotFound();
            }
            return View(find);
        }
        [HttpPost]
        public ActionResult EditTrang(Trang editedtrang)
        {
            if (ModelState.IsValid)
            {
                
                Trang trang = db.Trangs.FirstOrDefault(p => p.IDTrang == editedtrang.IDTrang);
                if (trang != null)
                {
                    trang.IDChapter = editedtrang.IDChapter;
                    trang.Image = editedtrang.Image;
                    trang.TSequence = editedtrang.TSequence;
                    db.Trangs.AddOrUpdate(trang);
                    db.SaveChanges();
                    return View("SuaChapters", Trang.GetTrangs(editedtrang.IDChapter));
                }
                else
                {
                    return View("SuaChapters", Trang.GetTrangs(editedtrang.IDChapter));
                }
            }
            else
            {
                return View("EditTrang", editedtrang);
            }
        }
        public ActionResult DeleteTrang(int id)
        {
            
            var find = db.Trangs.Where(x => x.IDTrang == id).First();
            if (find == null)
            {
                return HttpNotFound();
            }
            return View(find);
        }
        [HttpPost]
        public ActionResult DeleteTrang(int id, FormCollection collection)
        {
            
            Trang trang = db.Trangs.FirstOrDefault(x => x.IDTrang == id);
            int IDChapter = trang.IDChapter;
            if (trang == null)
            {
                return HttpNotFound();
            }
            db.Trangs.Remove(trang);
            db.SaveChanges();
            return View("SuaChapters", Trang.GetTrangs(IDChapter));
        }
        public ActionResult Create(int id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            
            var objm = db.Saches.Where(x => x.IDSach == id).First();
            var objc = db.Chapters.Where(x => x.IDSach == id).ToList();
            var ThemChapterV = new SuaTruyenViewModel
            {
                IDManga = id,
                NameManga = objm.TenSach,
                TotalChapters = objc.Count()
            };
            return View(ThemChapterV);
        }
        [HttpPost]
        public ActionResult Create(SuaTruyenViewModel newchapter)
        {
            if (newchapter.IDManga == null || newchapter.IDManga == 0)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                
                var chapter = new Chapter
                {
                    IDSach = newchapter.IDManga,
                    CSequence = newchapter.CSequence
                };
                db.Chapters.Add(chapter);
                db.SaveChanges();
                var ReferChapter = db.Chapters.Where(x => x.IDSach == newchapter.IDManga).ToList().OrderBy(x => x.CSequence).Last();
                var trang = new Trang
                {
                    IDChapter = ReferChapter.IDChapter,
                    TSequence = 1
                };
                db.Trangs.Add(trang);
                db.SaveChanges();
                return View("SuaTruyen", Chapter.GetChapters());
            }
            else
            {
                return View("Create", newchapter);
            }
        }
    }
}