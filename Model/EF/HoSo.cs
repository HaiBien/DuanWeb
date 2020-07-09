namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoSo")]
    public partial class HoSo
    {
        [Key]
        public int IDHoSo { get; set; }

        public int IDBenhNhan { get; set; }

        public int IDBacSi { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayKham { get; set; }

        [Required]
        public string KetQua { get; set; }

        [StringLength(10)]
        public string GhiChu { get; set; }

        public virtual Bacsi Bacsi { get; set; }

        public virtual BenhNhan BenhNhan { get; set; }
    }
}
