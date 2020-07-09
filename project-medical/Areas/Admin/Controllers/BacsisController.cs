using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using PagedList;

namespace project_medical.Areas.Admin.Controllers
{
    public class BacsisController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Admin/Bacsis
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            var bacsis = from s in db.Bacsis
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                bacsis = bacsis.Where(s => s.HoTen.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    bacsis = bacsis.OrderByDescending(s => s.HoTen);
                    break;

                default:  // Name ascending 
                    bacsis = bacsis.OrderBy(s => s.IDBacsi);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(bacsis.ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/Bacsis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bacsi bacsi = db.Bacsis.Find(id);
            if (bacsi == null)
            {
                return HttpNotFound();
            }
            return View(bacsi);
        }

        // GET: Admin/Bacsis/Create
        public ActionResult Create()
        {
            ViewBag.IDKhoa = new SelectList(db.Khoas, "IDKhoa", "TenKhoa");
            return View();
        }

        // POST: Admin/Bacsis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDBacsi,HoTen,Email,DienThoai,TaiKhoan,MatKhau,IDKhoa,Role")] Bacsi bacsi)
        {
            if (ModelState.IsValid)
            {
                db.Bacsis.Add(bacsi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDKhoa = new SelectList(db.Khoas, "IDKhoa", "TenKhoa", bacsi.IDKhoa);
            return View(bacsi);
        }

        // GET: Admin/Bacsis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bacsi bacsi = db.Bacsis.Find(id);
            if (bacsi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKhoa = new SelectList(db.Khoas, "IDKhoa", "TenKhoa", bacsi.IDKhoa);
            return View(bacsi);
        }

        // POST: Admin/Bacsis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBacsi,HoTen,Email,DienThoai,TaiKhoan,MatKhau,IDKhoa,Role")] Bacsi bacsi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bacsi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKhoa = new SelectList(db.Khoas, "IDKhoa", "TenKhoa", bacsi.IDKhoa);
            return View(bacsi);
        }

        // GET: Admin/Bacsis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bacsi bacsi = db.Bacsis.Find(id);
            if (bacsi == null)
            {
                return HttpNotFound();
            }
            return View(bacsi);
        }

        // POST: Admin/Bacsis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bacsi bacsi = db.Bacsis.Find(id);
            db.Bacsis.Remove(bacsi);
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
