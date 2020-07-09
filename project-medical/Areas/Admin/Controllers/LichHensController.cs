using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using PagedList;
namespace project_medical.Areas.Admin.Controllers
{
    public class LichHensController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Admin/LichHens
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            // var users = db.Users.Include(u => u.Quyen);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var qas = from s in db.LichHens
                      select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                qas = qas.Where(s => s.GhiChu.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    qas = qas.OrderByDescending(s => s.IDLich);
                    break;

                default:  // Name ascending 
                    qas = qas.OrderBy(s => s.IDLich);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(qas.ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/LichHens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichHen lichHen = db.LichHens.Find(id);
            if (lichHen == null)
            {
                return HttpNotFound();
            }
            return View(lichHen);
        }

        // GET: Admin/LichHens/Create
        public ActionResult Create()
        {
            ViewBag.IDBacSi = new SelectList(db.Bacsis, "IDBacsi", "HoTen");
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen");
            return View();
        }

        // POST: Admin/LichHens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLich,IDBenhNhan,IDBacSi,BatDau,KetThuc,GhiChu,TrangThai")] LichHen lichHen)
        {
            if (ModelState.IsValid)
            {
                db.LichHens.Add(lichHen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBacSi = new SelectList(db.Bacsis, "IDBacsi", "HoTen", lichHen.IDBacSi);
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", lichHen.IDBenhNhan);
            return View(lichHen);
        }

        // GET: Admin/LichHens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichHen lichHen = db.LichHens.Find(id);
            if (lichHen == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBacSi = new SelectList(db.Bacsis, "IDBacsi", "HoTen", lichHen.IDBacSi);
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", lichHen.IDBenhNhan);
            return View(lichHen);
        }

        // POST: Admin/LichHens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLich,IDBenhNhan,IDBacSi,BatDau,KetThuc,GhiChu,TrangThai")] LichHen lichHen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichHen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBacSi = new SelectList(db.Bacsis, "IDBacsi", "HoTen", lichHen.IDBacSi);
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", lichHen.IDBenhNhan);
            return View(lichHen);
        }

        // GET: Admin/LichHens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichHen lichHen = db.LichHens.Find(id);
            if (lichHen == null)
            {
                return HttpNotFound();
            }
            return View(lichHen);
        }

        // POST: Admin/LichHens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichHen lichHen = db.LichHens.Find(id);
            db.LichHens.Remove(lichHen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
