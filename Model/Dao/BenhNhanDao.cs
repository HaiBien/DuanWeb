using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    class BenhNhanDao
    {
        private ModelDbContext db = null;
        public BenhNhanDao()
        {
            db = new ModelDbContext();
        }


        public bool BenhNhan_Signup(BenhNhan entity)
        {
            try
            {
                entity.Role = 2;
                db.BenhNhans.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
