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
    public class TrungTamYTesController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

         //GET: Admin/TrungTamYTes
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

            var ttyts = from s in db.TrungTamYTes 
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                ttyts = ttyts.Where(s => s.TenTrungTam.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ttyts = ttyts.OrderByDescending(s => s.TenTrungTam);
                    break;

                default:  // Name ascending 
                    ttyts = ttyts.OrderBy(s => s.IDTrungTam);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ttyts.ToPagedList(pageNumber, pageSize));
        }

   

        // GET: Admin/TrungTamYTes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TrungTamYTe trungTamYTe = db.TrungTamYTes.Find(id);
        //    if (trungTamYTe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(trungTamYTe);
        //}

        // GET: Admin/TrungTamYTes/Create
        public ActionResult Create()
        {
            ViewBag.IDTinh = new SelectList(db.Tinhs, "IDTinh", "TenTinh");
            return View();
        }

        // POST: Admin/TrungTamYTes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTrungTam,TenTrungTam,DiaChi,IDTinh")] TrungTamYTe trungTamYTe)
        {
            if (ModelState.IsValid)
            {
                db.TrungTamYTes.Add(trungTamYTe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTinh = new SelectList(db.Tinhs, "IDTinh", "TenTinh", trungTamYTe.IDTinh);
            return View(trungTamYTe);
        }

        // GET: Admin/TrungTamYTes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrungTamYTe trungTamYTe = db.TrungTamYTes.Find(id);
            if (trungTamYTe == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTinh = new SelectList(db.Tinhs, "IDTinh", "TenTinh", trungTamYTe.IDTinh);
            return View(trungTamYTe);
        }

        // POST: Admin/TrungTamYTes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTrungTam,TenTrungTam,DiaChi,IDTinh")] TrungTamYTe trungTamYTe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trungTamYTe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTinh = new SelectList(db.Tinhs, "IDTinh", "TenTinh", trungTamYTe.IDTinh);
            return View(trungTamYTe);
        }

        // GET: Admin/TrungTamYTes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrungTamYTe trungTamYTe = db.TrungTamYTes.Find(id);
            if (trungTamYTe == null)
            {
                return HttpNotFound();
            }
            return View(trungTamYTe);
        }

        // POST: Admin/TrungTamYTes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrungTamYTe trungTamYTe = db.TrungTamYTes.Find(id);
            db.TrungTamYTes.Remove(trungTamYTe);
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
