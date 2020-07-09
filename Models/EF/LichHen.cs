namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichHen")]
    public partial class LichHen
    {
        [Key]
        public int IDLich { get; set; }

        public int IDBenhNhan { get; set; }

        public int IDBacSi { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime BatDau { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? KetThuc { get; set; }

        public string GhiChu { get; set; }

        public int TrangThai { get; set; }

        public virtual Bacsi Bacsi { get; set; }

        public virtual BenhNhan BenhNhan { get; set; }
    }
}
