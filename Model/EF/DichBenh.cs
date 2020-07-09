namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DichBenh")]
    public partial class DichBenh
    {
        [Key]
        public int IDDichBenh { get; set; }

        [Required]
        [StringLength(500)]
        public string TenDich { get; set; }

        [Required]
        [StringLength(50)]
        public string PhamVi { get; set; }

        public int SoCaMac { get; set; }

        public int TuVong { get; set; }

        public int DaKhoi { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgCapNhap { get; set; }
    }
}
