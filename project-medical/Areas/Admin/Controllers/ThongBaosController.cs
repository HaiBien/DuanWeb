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
    public class ThongBaosController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Admin/ThongBaos
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

            var qas = from s in db.ThongBaos
                      select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                qas = qas.Where(s => s.NoiDung.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    qas = qas.OrderByDescending(s => s.NoiDung);
                    break;

                default:  // Name ascending 
                    qas = qas.OrderBy(s => s.IDThongBao);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(qas.ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/ThongBaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongBao thongBao = db.ThongBaos.Find(id);
            if (thongBao == null)
            {
                return HttpNotFound();
            }
            return View(thongBao);
        }

        // GET: Admin/ThongBaos/Create
        public ActionResult Create()
        {
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen");
            return View();
        }

        // POST: Admin/ThongBaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDThongBao,NoiDung,IDBenhNhan,NgThongBao")] ThongBao thongBao)
        {
            if (ModelState.IsValid)
            {
                db.ThongBaos.Add(thongBao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", thongBao.IDBenhNhan);
            return View(thongBao);
        }

        // GET: Admin/ThongBaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongBao thongBao = db.ThongBaos.Find(id);
            if (thongBao == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", thongBao.IDBenhNhan);
            return View(thongBao);
        }

        // POST: Admin/ThongBaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDThongBao,NoiDung,IDBenhNhan,NgThongBao")] ThongBao thongBao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongBao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", thongBao.IDBenhNhan);
            return View(thongBao);
        }

        // GET: Admin/ThongBaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongBao thongBao = db.ThongBaos.Find(id);
            if (thongBao == null)
            {
                return HttpNotFound();
            }
            return View(thongBao);
        }

        // POST: Admin/ThongBaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongBao thongBao = db.ThongBaos.Find(id);
            db.ThongBaos.Remove(thongBao);
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
