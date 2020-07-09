using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace project_medical.Controllers
{
    public class TinTucController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: TinTuc
        public ActionResult Index(int IDTinTuc)
        {
            var model = db.TinTucs.Find(IDTinTuc);
            return View(model);
        }
    }
}