namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrungTamYTe")]
    public partial class TrungTamYTe
    {
        [Key]
        public int IDTrungTam { get; set; }

        [Required]
        public string TenTrungTam { get; set; }

        [Required]
        public string DiaChi { get; set; }

        public int IDTinh { get; set; }

        public virtual Tinh Tinh { get; set; }
    }
}
