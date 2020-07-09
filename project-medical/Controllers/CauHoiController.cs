using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;


namespace project_medical.Controllers
{
    public class CauHoiController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: CauHoi
        public HoiDap HoiDap_FindId(int IDCauHoi)
        {
            return db.HoiDaps.Find(IDCauHoi);
        }

        public ActionResult IndexCH(int IDCauHoi)
        {
            var model = HoiDap_FindId(IDCauHoi);
            return View(model);
        }
    }
}