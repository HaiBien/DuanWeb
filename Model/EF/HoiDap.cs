namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoiDap")]
    public partial class HoiDap
    {
        [Key]
        public int IDCauHoi { get; set; }

        public int IDBenhNhan { get; set; }

        [Required]
        [StringLength(500)]
        public string CauHoi { get; set; }

        public string TraLoi { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayHoi { get; set; }

        public int? TrangThai { get; set; }

        public virtual BenhNhan BenhNhan { get; set; }
    }
}
