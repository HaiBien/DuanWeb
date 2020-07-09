namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        public int IDTinTuc { get; set; }

        [Required]
        public string TieuDe { get; set; }

        [Required]
        public string NoiDung { get; set; }

        [Required]
        public string MoTa { get; set; }

        [Required]
        public string img { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayDang { get; set; }
    }
}
