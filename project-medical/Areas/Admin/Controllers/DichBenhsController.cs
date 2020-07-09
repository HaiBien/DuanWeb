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
    public class DichBenhsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Admin/DichBenhs
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

            var dichbenhs = from s in db.DichBenhs
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                dichbenhs = dichbenhs.Where(s => s.TenDich.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    dichbenhs = dichbenhs.OrderByDescending(s => s.TenDich);
                    break;

                default:  // Name ascending 
                    dichbenhs = dichbenhs.OrderBy(s => s.IDDichBenh);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dichbenhs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/DichBenhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichBenh dichBenh = db.DichBenhs.Find(id);
            if (dichBenh == null)
            {
                return HttpNotFound();
            }
            return View(dichBenh);
        }

        // GET: Admin/DichBenhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DichBenhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDichBenh,TenDich,PhamVi,SoCaMac,TuVong,DaKhoi,NgCapNhap")] DichBenh dichBenh)
        {
            if (ModelState.IsValid)
            {
                db.DichBenhs.Add(dichBenh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dichBenh);
        }

        // GET: Admin/DichBenhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichBenh dichBenh = db.DichBenhs.Find(id);
            if (dichBenh == null)
            {
                return HttpNotFound();
            }
            return View(dichBenh);
        }

        // POST: Admin/DichBenhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDichBenh,TenDich,PhamVi,SoCaMac,TuVong,DaKhoi,NgCapNhap")] DichBenh dichBenh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dichBenh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dichBenh);
        }

        // GET: Admin/DichBenhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichBenh dichBenh = db.DichBenhs.Find(id);
            if (dichBenh == null)
            {
                return HttpNotFound();
            }
            return View(dichBenh);
        }

        // POST: Admin/DichBenhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DichBenh dichBenh = db.DichBenhs.Find(id);
            db.DichBenhs.Remove(dichBenh);
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
