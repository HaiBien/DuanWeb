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
    public class BenhNhansController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Admin/BenhNhans
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

            var benhnhans = from s in db.BenhNhans
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                benhnhans = benhnhans.Where(s => s.HoTen.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    benhnhans = benhnhans.OrderByDescending(s => s.HoTen);
                    break;

                default:  // Name ascending 
                    benhnhans = benhnhans.OrderBy(s => s.IDBenhNhan);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(benhnhans.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/BenhNhans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BenhNhan benhNhan = db.BenhNhans.Find(id);
            if (benhNhan == null)
            {
                return HttpNotFound();
            }
            return View(benhNhan);
        }

        // GET: Admin/BenhNhans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BenhNhans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDBenhNhan,HoTen,GioiTinh,NamSinh,Email,DienThoai,DiaChi,CMND,TaiKhoan,MatKhau,Role")] BenhNhan benhNhan)
        {
            if (ModelState.IsValid)
            {
                db.BenhNhans.Add(benhNhan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(benhNhan);
        }

        // GET: Admin/BenhNhans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BenhNhan benhNhan = db.BenhNhans.Find(id);
            if (benhNhan == null)
            {
                return HttpNotFound();
            }
            return View(benhNhan);
        }

        // POST: Admin/BenhNhans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBenhNhan,HoTen,GioiTinh,NamSinh,Email,DienThoai,DiaChi,CMND,TaiKhoan,MatKhau,Role")] BenhNhan benhNhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(benhNhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(benhNhan);
        }

        // GET: Admin/BenhNhans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BenhNhan benhNhan = db.BenhNhans.Find(id);
            if (benhNhan == null)
            {
                return HttpNotFound();
            }
            return View(benhNhan);
        }

        // POST: Admin/BenhNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BenhNhan benhNhan = db.BenhNhans.Find(id);
            db.BenhNhans.Remove(benhNhan);
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
