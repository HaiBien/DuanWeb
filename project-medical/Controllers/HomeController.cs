using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;


namespace project_medical.Controllers
{
    public class HomeController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public List<HoiDap> HoiDap_FindAll(int iD)
        {
            return db.HoiDaps.Where(t => t.IDBenhNhan == iD).OrderByDescending(x => x.IDCauHoi).ToList();
        }
        public ActionResult ThacMacBN()
        {
            return PartialView();
        }
        public List<LichHen> LichHen_FindAll(int iD)
        {
            return db.LichHens.Where(t => t.IDBenhNhan == iD).OrderByDescending(x => x.IDLich).ToList();
        }
        public ActionResult LichHenBN()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(FormCollection useForm)
        {
            String userName = useForm["userName"].ToString();
            String password = useForm["password"].ToString();

            var login = db.BenhNhans.SingleOrDefault(u => u.TaiKhoan == userName && u.MatKhau == password);
            var loginAdmin = db.Bacsis.SingleOrDefault(u => u.TaiKhoan == userName && u.MatKhau == password && ( u.Role == 1 || u.Role ==2) );
            if (loginAdmin != null)
            {
                Session["useView"] = loginAdmin;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (login != null)
                {
                    Session["useView"] = login;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Fail = "Tài khoản hoặc mật khẩu chưa chính xác!";
                }
            }
            return View();
        }
        public ActionResult Dangxuat()
        {
            Session["useView"] = null;
            return RedirectToAction("Index", "Home");
        }
        // SIGNUP
        public bool User_Signup(BenhNhan entity)
        {
            try
            {
                entity.Role = 0;
                db.BenhNhans.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(BenhNhan entity)
        {
      
            User_Signup(entity);
            return RedirectToAction("Index");

        }

        
        // END SIGNUP
        public ActionResult CreateLichHen()
        {
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen");
            ViewBag.IDBacsi = new SelectList(db.Bacsis, "IDBacsi", "HoTen");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLichHen([Bind(Include = "IDLich,IDBenhNhan,IDBacSi,BatDau,KetThuc,GhiChu,TrangThai")] LichHen lichHen)
        {
            if (ModelState.IsValid)
            {
                db.LichHens.Add(lichHen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBacSi = new SelectList(db.Bacsis, "IDBacsi", "HoTen", lichHen.IDBacSi);
            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", lichHen.IDBenhNhan);
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreateHoiDap()
        {
            return View();
        }
        DateTime d1 = DateTime.Now;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHoiDap([Bind(Include = "IDCauHoi,IDBenhNhan,CauHoi,TraLoi,NgayHoi")] HoiDap hoiDap)
        {
            if (ModelState.IsValid)
            {
                hoiDap.NgayHoi = d1;
                hoiDap.TrangThai = 0;
                db.HoiDaps.Add(hoiDap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBenhNhan = new SelectList(db.BenhNhans, "IDBenhNhan", "HoTen", hoiDap.IDBenhNhan);
            return View(hoiDap);
        }

        public List<LichHen> LichHen_FindAllBS(int iD)
        {
            return db.LichHens.Where(t => t.IDBacSi == iD && t.TrangThai == 0).OrderByDescending(x => x.IDLich).ToList();
        }
        public ActionResult XacNhanLichHen(int IDBacsi)
        {
            var lichHens = LichHen_FindAllBS(IDBacsi);
            return PartialView(lichHens);
        }


        public bool LichHen_Update(LichHen entity)
        {
            try
            {
                var model = db.LichHens.Find(entity.IDLich);
                if (model != null)
                {

                    model.IDBacSi = model.IDBacSi;
                    model.IDBenhNhan = model.IDBenhNhan;
                    model.BatDau = entity.BatDau;
                    model.KetThuc = entity.KetThuc;
                    model.GhiChu = entity.GhiChu;
                    model.IDZoom = "576-869-5375";
                    model.MkZoom = "123456";
                    model.LinkZoom = "https://us04web.zoom.us/j/5768695375?pwd=ZzJPOE5uVnVvb1FvRGQ5ZmFLd2Z2dz09 ";
                    model.TrangThai = 1;


                    db.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public LichHen LichHen_FindOneById(int IDLich)
        {
            return db.LichHens.Find(IDLich);
        }

        [HttpGet]
        public ActionResult XacNhanLH(int IDLich)
        {
            var model = LichHen_FindOneById(IDLich);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XacNhanLH(LichHen entity)
        {
            if (LichHen_Update(entity) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(entity);
            }
        }

        public List<LichHen> LichHen_Find(int iD)
        {
            return db.LichHens.Where(t => t.IDBacSi == iD && t.TrangThai == 1).OrderByDescending(x => x.IDLich).ToList();
        }
        public ActionResult LichHenTuVan(int IDBacsi)
        {
            var lichHens = LichHen_Find(IDBacsi);
            return PartialView(lichHens);
        }

        public List<HoiDap> HoiDap_Find()
        {
            return db.HoiDaps.Where(t => t.TrangThai == 0).OrderBy(x => x.NgayHoi).ToList();
        }
        public ActionResult GiaiDapThacMac()
        {
            var hoiDaps = HoiDap_Find();
            return PartialView(hoiDaps);
        }



        public bool TraLoiCauHoi_Update(HoiDap entity)
        {
            try
            {
                var model = db.HoiDaps.Find(entity.IDCauHoi);
                if (model != null)
                {

                    model.IDBenhNhan = model.IDBenhNhan;
                    model.CauHoi = model.CauHoi;
                    model.TraLoi = entity.TraLoi;
                    model.TrangThai = 1;


                    db.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public LichHen TraLoiCauHoi_FindOneById(int IDCauHoi)
        {
            return db.LichHens.Find(IDCauHoi);
        }
        [HttpGet]
        public ActionResult TraLoiCauHoi(int IDCauHoi)
        {
            var model = TraLoiCauHoi_FindOneById(IDCauHoi);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TraLoiCauHoi(HoiDap entity)
        {
            if (TraLoiCauHoi_Update(entity) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(entity);
            }
        }
    }
}