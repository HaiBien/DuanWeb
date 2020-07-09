using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using System.Data.Entity;


namespace project_medical.Controllers
{
    public class PartialController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: Partial

        public ActionResult PartialList(int iD)
        {
            //  var lists = from t in dao.TinTuc_FindAll().ToList().Take(8) select t;
            var hoiDaps = db.HoiDaps.Where(t => t.IDBenhNhan == iD).OrderByDescending(x => x.IDCauHoi);          
            return PartialView(hoiDaps.ToList());
        }
        public ActionResult PartialTinTuc()
        {
            //  var lists = from t in dao.TinTuc_FindAll().ToList().Take(8) select t;
            var tinTucs = db.TinTucs.OrderByDescending(x => x.IDTinTuc);
            return PartialView(tinTucs.ToList().Take(3));
        }
        public ActionResult PartialThacMacBS()
        {
            //  var lists = from t in dao.TinTuc_FindAll().ToList().Take(8) select t;
            var hoiDaps = db.HoiDaps.Where(x => x.TrangThai == 0).OrderByDescending(x => x.IDCauHoi);          
            return PartialView(hoiDaps.ToList ());
        }
        public ActionResult PartialLichHen()
        {
          //  var lists = from t in dao.TinTuc_FindAll().ToList().Take(8) select t;
            var list = db.LichHens.OrderByDescending(x =>x.TrangThai).Include(l => l.Bacsi).Include(l => l.BenhNhan);
            return PartialView(list.ToList());
        }

        //public ActionResult PartialGame()
        //{
        //    var Loaiss = db.HoiDaps.Where(x => x.TrangThai == 1).ToList().Take(6);
        //    //var dienThoais = from t in dao.DienThoai_Hang_FindAll(1).ToList().Take(8) select t;
        //    return PartialView(Loaiss);
        //}


    }
}