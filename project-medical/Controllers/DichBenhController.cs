using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace project_medical.Controllers
{
    public class DichBenhController : Controller
    {
        // GET: DichBenh
        ModelDbContext db = new ModelDbContext();
        public List<DichBenh> DichBenh_FindAll()
        {
            return db.DichBenhs.OrderByDescending(x => x.IDDichBenh).ToList();
        }
        public ActionResult DichBenh()
        {
            var lists = from t in DichBenh_FindAll().ToList().Take(2) select t;
            return PartialView(lists);
        }


    }
}