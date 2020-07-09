namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongBao")]
    public partial class ThongBao
    {
        [Key]
        public int IDThongBao { get; set; }

        [StringLength(500)]
        public string NoiDung { get; set; }

        public int IDBenhNhan { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgThongBao { get; set; }

        public virtual BenhNhan BenhNhan { get; set; }
    }
}
