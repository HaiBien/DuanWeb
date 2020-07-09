namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bacsi")]
    public partial class Bacsi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bacsi()
        {
            HoSoes = new HashSet<HoSo>();
            LichHens = new HashSet<LichHen>();
        }

        [Key]
        public int IDBacsi { get; set; }

        [Required]
        [StringLength(500)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(500)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string DienThoai { get; set; }

        [Required]
        [StringLength(30)]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(30)]
        public string MatKhau { get; set; }

        public int IDKhoa { get; set; }

        public int Role { get; set; }

        public virtual Khoa Khoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoSo> HoSoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichHen> LichHens { get; set; }
    }
}
