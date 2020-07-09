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
    public class HoiDapsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Admin/HoiDaps
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

            var qas = from s in db.HoiDaps
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                qas = qas.Where(s => s.CauHoi.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    qas = qas.OrderByDescending(s => s.CauHoi);
                    break;

                default:  // Name ascending 
                    qas = qas.OrderBy(s => s.IDCauHoi);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(qas.ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/HoiDaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoiDap hoiDap = db.HoiDaps.Find(id);
            if (hoiDap == null)
            {
                return HttpNotFound();
            }
            return View(hoiDap);
        }

        // GET: Admin/HoiDaps/Create
        public ActionResult Create()
        {
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen");
            return View();
        }

        // POST: Admin/HoiDaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCauHoi,IDBenhNhan,CauHoi,TraLoi,NgayHoi")] HoiDap hoiDap)
        {
            if (ModelState.IsValid)
            {
                db.HoiDaps.Add(hoiDap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", hoiDap.IDBenhNhan);
            return View(hoiDap);
        }

        // GET: Admin/HoiDaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoiDap hoiDap = db.HoiDaps.Find(id);
            if (hoiDap == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", hoiDap.IDBenhNhan);
            return View(hoiDap);
        }

        // POST: Admin/HoiDaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCauHoi,IDBenhNhan,CauHoi,TraLoi,NgayHoi")] HoiDap hoiDap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoiDap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", hoiDap.IDBenhNhan);
            return View(hoiDap);
        }

        // GET: Admin/HoiDaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoiDap hoiDap = db.HoiDaps.Find(id);
            if (hoiDap == null)
            {
                return HttpNotFound();
            }
            return View(hoiDap);
        }

        // POST: Admin/HoiDaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoiDap hoiDap = db.HoiDaps.Find(id);
            db.HoiDaps.Remove(hoiDap);
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
