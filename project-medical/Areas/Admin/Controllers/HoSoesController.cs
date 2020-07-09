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
    public class HoSoesController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Admin/HoSoes
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

            var hosoes = from s in db.HoSoes
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                hosoes = hosoes.Where(s => s.KetQua.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    hosoes = hosoes.OrderByDescending(s => s.KetQua);
                    break;

                default:  // Name ascending 
                    hosoes = hosoes.OrderBy(s => s.IDHoSo);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(hosoes.ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/HoSoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSo hoSo = db.HoSoes.Find(id);
            if (hoSo == null)
            {
                return HttpNotFound();
            }
            return View(hoSo);
        }

        // GET: Admin/HoSoes/Create
        public ActionResult Create()
        {
            ViewBag.IDBacSi = new SelectList(db.Bacsis, "IDBacsi", "HoTen");
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen");
            return View();
        }

        // POST: Admin/HoSoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHoSo,IDBenhNhan,IDBacSi,NgayKham,KetQua,GhiChu")] HoSo hoSo)
        {
            if (ModelState.IsValid)
            {
                db.HoSoes.Add(hoSo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBacSi = new SelectList(db.Bacsis, "IDBacsi", "HoTen", hoSo.IDBacSi);
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", hoSo.IDBenhNhan);
            return View(hoSo);
        }

        // GET: Admin/HoSoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSo hoSo = db.HoSoes.Find(id);
            if (hoSo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBacSi = new SelectList(db.Bacsis, "IDBacsi", "HoTen", hoSo.IDBacSi);
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", hoSo.IDBenhNhan);
            return View(hoSo);
        }

        // POST: Admin/HoSoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHoSo,IDBenhNhan,IDBacSi,NgayKham,KetQua,GhiChu")] HoSo hoSo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoSo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBacSi = new SelectList(db.Bacsis, "IDBacsi", "HoTen", hoSo.IDBacSi);
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", hoSo.IDBenhNhan);
            return View(hoSo);
        }

        // GET: Admin/HoSoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSo hoSo = db.HoSoes.Find(id);
            if (hoSo == null)
            {
                return HttpNotFound();
            }
            return View(hoSo);
        }

        // POST: Admin/HoSoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoSo hoSo = db.HoSoes.Find(id);
            db.HoSoes.Remove(hoSo);
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
