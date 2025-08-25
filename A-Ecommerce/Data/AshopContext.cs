using Microsoft.EntityFrameworkCore;

namespace A_Ecommerce.Data
{
    public partial class AShopContext : DbContext
    {
        public AShopContext()
        {
        }

        public AShopContext(DbContextOptions<AShopContext> options)
            : base(options)
        {
        }

        // Các DbSet
        public virtual DbSet<BanBe> BanBes { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=AShop;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Bảng BanBe
            modelBuilder.Entity<BanBe>(entity =>
            {
                entity.HasKey(e => e.MaBb);
                entity.ToTable("BanBe");

                entity.Property(e => e.MaBb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaBB");
                entity.Property(e => e.TenBb)
                    .HasMaxLength(50)
                    .HasColumnName("TenBB");
            });

            // Bảng SanPham
            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);
                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaSP");
                entity.Property(e => e.TenSp)
                    .HasMaxLength(100)
                    .HasColumnName("TenSP");
                entity.Property(e => e.Gia).HasColumnType("decimal(18,2)");
            });

            // Bảng KhachHang
            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);
                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaKH");
                entity.Property(e => e.TenKh)
                    .HasMaxLength(100)
                    .HasColumnName("TenKH");
            });

            // Bảng Loai
            modelBuilder.Entity<Loai>(entity =>
            {
                entity.HasKey(e => e.MaLoai);
                entity.ToTable("Loai");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.TenLoai)
                    .HasMaxLength(100);
            });

            // Bảng NhaCungCap
            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNcc);
                entity.ToTable("NhaCungCap");

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaNCC");
                entity.Property(e => e.TenNcc)
                    .HasMaxLength(100)
                    .HasColumnName("TenNCC");
            });

            // Bảng HangHoa
            modelBuilder.Entity<HangHoa>(entity =>
            {
                entity.HasKey(e => e.MaSp);
                entity.ToTable("HangHoa");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaSP");
                entity.Property(e => e.TenSp)
                    .HasMaxLength(100)
                    .HasColumnName("TenSP");
                entity.Property(e => e.Gia).HasColumnType("decimal(18,2)");

                // FK Loai
                entity.HasOne(d => d.MaLoaiNavigation)
                      .WithMany(p => p.HangHoas)
                      .HasForeignKey(d => d.MaLoai);

                // FK NhaCungCap
                entity.HasOne(d => d.MaNccNavigation)
                      .WithMany(p => p.HangHoas)
                      .HasForeignKey(d => d.MaNcc);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
