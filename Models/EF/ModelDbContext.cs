namespace Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Bacsi> Bacsis { get; set; }
        public virtual DbSet<BenhNhan> BenhNhans { get; set; }
        public virtual DbSet<DichBenh> DichBenhs { get; set; }
        public virtual DbSet<HoiDap> HoiDaps { get; set; }
        public virtual DbSet<HoSo> HoSoes { get; set; }
        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<LichHen> LichHens { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }
        public virtual DbSet<Tinh> Tinhs { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }
        public virtual DbSet<TrungTamYTe> TrungTamYTes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bacsi>()
                .Property(e => e.DienThoai)
                .IsFixedLength();

            modelBuilder.Entity<Bacsi>()
                .Property(e => e.TaiKhoan)
                .IsFixedLength();

            modelBuilder.Entity<Bacsi>()
                .Property(e => e.MatKhau)
                .IsFixedLength();

            modelBuilder.Entity<Bacsi>()
                .HasMany(e => e.HoSoes)
                .WithRequired(e => e.Bacsi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bacsi>()
                .HasMany(e => e.LichHens)
                .WithRequired(e => e.Bacsi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BenhNhan>()
                .Property(e => e.DienThoai)
                .IsFixedLength();

            modelBuilder.Entity<BenhNhan>()
                .Property(e => e.CMND)
                .IsFixedLength();

            modelBuilder.Entity<BenhNhan>()
                .Property(e => e.TaiKhoan)
                .IsFixedLength();

            modelBuilder.Entity<BenhNhan>()
                .Property(e => e.MatKhau)
                .IsFixedLength();

            modelBuilder.Entity<BenhNhan>()
                .HasMany(e => e.HoiDaps)
                .WithRequired(e => e.BenhNhan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BenhNhan>()
                .HasMany(e => e.HoSoes)
                .WithRequired(e => e.BenhNhan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BenhNhan>()
                .HasMany(e => e.LichHens)
                .WithRequired(e => e.BenhNhan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BenhNhan>()
                .HasMany(e => e.ThongBaos)
                .WithRequired(e => e.BenhNhan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoiDap>()
                .Property(e => e.CauHoi)
                .IsFixedLength();

            modelBuilder.Entity<HoiDap>()
                .Property(e => e.TraLoi)
                .IsFixedLength();

            modelBuilder.Entity<HoSo>()
                .Property(e => e.GhiChu)
                .IsFixedLength();

            modelBuilder.Entity<Khoa>()
                .HasMany(e => e.Bacsis)
                .WithRequired(e => e.Khoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tinh>()
                .HasMany(e => e.TrungTamYTes)
                .WithRequired(e => e.Tinh)
                .WillCascadeOnDelete(false);
        }
    }
}
