namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangNhap")]
    public partial class DangNhap
    {
        [Key]
        [Column(Order = 0)]
        public string HoTen { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string TaiKhoan { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string MatKhau { get; set; }

        public int? Role { get; set; }
    }
}
