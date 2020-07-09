using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace project_medical.Controllers
{
    public class LichHenPartialViewController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        public List<LichHen> LichHen_FindAll(int iD)
        {
            return db.LichHens.Where(t => t.IDBenhNhan == iD).OrderByDescending(x => x.IDLich).ToList();
        }
        public ActionResult LichHenFindAll(int IDBenhNhan)
        {
            var lichHens = LichHen_FindAll(IDBenhNhan);
            return PartialView(lichHens);
        }
        public List<LichHen> LichHen_FindHoanThanh(int iD)
        {
            return db.LichHens.Where(t => t.IDBenhNhan == iD && t.TrangThai == -1).OrderByDescending(x => x.IDLich).ToList();
        }
        public ActionResult LichHenFindHoanThanh(int IDBenhNhan)
        {
            var lichHens = LichHen_FindHoanThanh(IDBenhNhan);
            return PartialView(lichHens);
        }
        public List<LichHen> LichHen_FindDangXuLy(int iD)
        {
            return db.LichHens.Where(t => t.IDBenhNhan == iD && t.TrangThai == 0).OrderByDescending(x => x.IDLich).ToList();
        }
        public ActionResult LichHenFindDangXuLy(int IDBenhNhan)
        {
            var lichHens = LichHen_FindDangXuLy(IDBenhNhan);
            return PartialView(lichHens);
        }
    }
}