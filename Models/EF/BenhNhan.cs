namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BenhNhan")]
    public partial class BenhNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BenhNhan()
        {
            HoiDaps = new HashSet<HoiDap>();
            HoSoes = new HashSet<HoSo>();
            LichHens = new HashSet<LichHen>();
            ThongBaos = new HashSet<ThongBao>();
        }

        [Key]
        public int IDBenhNhan { get; set; }

        [Required]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string GioiTinh { get; set; }

        public int? NamSinh { get; set; }

        [Required]
        public string Email { get; set; }

        [StringLength(20)]
        public string DienThoai { get; set; }

        public string DiaChi { get; set; }

        [StringLength(20)]
        public string CMND { get; set; }

        [Required]
        [StringLength(30)]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(30)]
        public string MatKhau { get; set; }

        public int? Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoiDap> HoiDaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoSo> HoSoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichHen> LichHens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
