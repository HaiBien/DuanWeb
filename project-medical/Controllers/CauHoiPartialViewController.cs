using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace project_medical.Controllers
{
    public class CauHoiPartialViewController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        public List<HoiDap> HoiDap_FindAll(int iD)
        {
            return db.HoiDaps.Where(t => t.IDBenhNhan == iD).OrderByDescending(x => x.IDCauHoi).ToList();
        }
        public ActionResult HoiDapFindAll(int IDBenhNhan)
        {
            var hoiDaps = HoiDap_FindAll(IDBenhNhan);
            return PartialView(hoiDaps);
        }
        public List<HoiDap> HoiDap_FindDaTraLoi(int iD)
        {
            return db.HoiDaps.Where(t => t.IDBenhNhan == iD && t.TrangThai == 1).OrderByDescending(x => x.IDCauHoi).ToList();
        }
        public ActionResult HoiDapFindDaTraLoi(int IDBenhNhan)
        {
            var HoiDaps = HoiDap_FindDaTraLoi(IDBenhNhan);
            return PartialView(HoiDaps);
        }
        public List<HoiDap> HoiDap_FindDangXuLy(int iD)
        {
            return db.HoiDaps.Where(t => t.IDBenhNhan == iD && t.TrangThai == 0).OrderByDescending(x => x.IDCauHoi).ToList();
        }
        public ActionResult HoiDapFindDangXuLy(int IDBenhNhan)
        {
            var HoiDaps = HoiDap_FindDangXuLy(IDBenhNhan);
            return PartialView(HoiDaps);
        }
    }
}