using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Register_Patient_Online.Models
{
    public partial class RegisterPatientOnlineContext : DbContext
    {
        public RegisterPatientOnlineContext()
        {
        }

        public RegisterPatientOnlineContext(DbContextOptions<RegisterPatientOnlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BacSi> BacSis { get; set; } = null!;
        public virtual DbSet<BaoHiemYTe> BaoHiemYTes { get; set; } = null!;
        public virtual DbSet<BenhNhan> BenhNhans { get; set; } = null!;
        public virtual DbSet<DangKyKham> DangKyKhams { get; set; } = null!;
        public virtual DbSet<KhoaKhamBenh> KhoaKhamBenhs { get; set; } = null!;
        public virtual DbSet<LichLamViec> LichLamViecs { get; set; } = null!;
        public virtual DbSet<QuanLy> QuanLies { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public virtual DbSet<TinTuc> TinTucs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-B4QAIC4U\\MSSQLSERVER01;Initial Catalog=RegisterPatientOnline;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BacSi>(entity =>
            {
                entity.HasKey(e => e.MaBs)
                    .HasName("PK__BAC_SI__53E64EC99B59EB88");

                entity.ToTable("BAC_SI");

                entity.HasIndex(e => e.MaTk, "UQ__BAC_SI__53E1C493373E7D50")
                    .IsUnique();

                entity.Property(e => e.MaBs)
                    .HasMaxLength(100)
                    .HasColumnName("MA_BS");

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .HasColumnName("MA_KHOA");

                entity.Property(e => e.MaTk).HasColumnName("MA_TK");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("TEN");

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.MaKhoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BAC_SI__MA_KHOA__4CA06362");

                entity.HasOne(d => d.MaTkNavigation)
                    .WithOne(p => p.BacSi)
                    .HasForeignKey<BacSi>(d => d.MaTk)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__BAC_SI__MA_TK__4BAC3F29");
            });

            modelBuilder.Entity<BaoHiemYTe>(entity =>
            {
                entity.HasKey(e => e.MaBhyt)
                    .HasName("PK__BAO_HIEM__6B0F5FC2563E692F");

                entity.ToTable("BAO_HIEM_Y_TE");

                entity.Property(e => e.MaBhyt)
                    .HasMaxLength(15)
                    .HasColumnName("MA_BHYT");

                entity.Property(e => e.GioiTinh).HasColumnName("GIOI_TINH");

                entity.Property(e => e.NgayBd)
                    .HasColumnType("date")
                    .HasColumnName("NGAY_BD");

                entity.Property(e => e.NgayHh)
                    .HasColumnType("date")
                    .HasColumnName("NGAY_HH");

                entity.Property(e => e.NgaySinh)
                    .HasColumnType("date")
                    .HasColumnName("NGAY_SINH");
            });

            modelBuilder.Entity<BenhNhan>(entity =>
            {
                entity.HasKey(e => e.MaBn)
                    .HasName("PK__BENH_NHA__53E64ECE9AD19C13");

                entity.ToTable("BENH_NHAN");

                entity.HasIndex(e => e.MaTk, "UQ__BENH_NHA__53E1C4938E3ADDEB")
                    .IsUnique();

                entity.HasIndex(e => e.MaBhyt, "UQ__BENH_NHA__6B0F5FC3A3FFED80")
                    .IsUnique();

                entity.Property(e => e.MaBn)
                    .HasMaxLength(100)
                    .HasColumnName("MA_BN");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(15)
                    .HasColumnName("CCCD");

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(255)
                    .HasColumnName("DIA_CHI");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(255)
                    .HasColumnName("HOTEN");

                entity.Property(e => e.MaBhyt)
                    .HasMaxLength(15)
                    .HasColumnName("MA_BHYT");

                entity.Property(e => e.MaTk).HasColumnName("MA_TK");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .HasColumnName("SDT");

                entity.HasOne(d => d.MaBhytNavigation)
                    .WithOne(p => p.BenhNhan)
                    .HasForeignKey<BenhNhan>(d => d.MaBhyt)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__BENH_NHAN__MA_BH__4316F928");

                entity.HasOne(d => d.MaTkNavigation)
                    .WithOne(p => p.BenhNhan)
                    .HasForeignKey<BenhNhan>(d => d.MaTk)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__BENH_NHAN__MA_TK__4222D4EF");
            });

            modelBuilder.Entity<DangKyKham>(entity =>
            {
                entity.HasKey(e => e.MaDk)
                    .HasName("PK__DANG_KY___53E15A86FD2BE639");

                entity.ToTable("DANG_KY_KHAM");

                entity.Property(e => e.MaDk).HasColumnName("MA_DK");

                entity.Property(e => e.MaBn)
                    .HasMaxLength(100)
                    .HasColumnName("MA_BN");

                entity.Property(e => e.MaBs)
                    .HasMaxLength(100)
                    .HasColumnName("MA_BS");

                entity.Property(e => e.NgayDangKi)
                    .HasColumnType("date")
                    .HasColumnName("NGAY_DANG_KI");

                entity.Property(e => e.NgayDenKham)
                    .HasColumnType("date")
                    .HasColumnName("NGAY_DEN_KHAM");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(50)
                    .HasColumnName("TRANG_THAI")
                    .HasDefaultValueSql("('CH? XÁC NH?N')");

                entity.HasOne(d => d.MaBnNavigation)
                    .WithMany(p => p.DangKyKhams)
                    .HasForeignKey(d => d.MaBn)
                    .HasConstraintName("FK__DANG_KY_K__MA_BN__534D60F1");

                entity.HasOne(d => d.MaBsNavigation)
                    .WithMany(p => p.DangKyKhams)
                    .HasForeignKey(d => d.MaBs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DANG_KY_K__MA_BS__5441852A");
            });

            modelBuilder.Entity<KhoaKhamBenh>(entity =>
            {
                entity.HasKey(e => e.MaKhoa)
                    .HasName("PK__KHOA_KHA__65A8F1620FE48049");

                entity.ToTable("KHOA_KHAM_BENH");

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .HasColumnName("MA_KHOA");

                entity.Property(e => e.TenKhoa)
                    .HasMaxLength(100)
                    .HasColumnName("TEN_KHOA");
            });

            modelBuilder.Entity<LichLamViec>(entity =>
            {
                entity.HasKey(e => new { e.MaBs, e.Ngay, e.CaLamViec })
                    .HasName("PK__LICH_LAM__5AF2CD07D5F4920E");

                entity.ToTable("LICH_LAM_VIEC");

                entity.Property(e => e.MaBs)
                    .HasMaxLength(100)
                    .HasColumnName("MA_BS");

                entity.Property(e => e.Ngay)
                    .HasColumnType("date")
                    .HasColumnName("NGAY");

                entity.Property(e => e.CaLamViec)
                    .HasMaxLength(20)
                    .HasColumnName("CA_LAM_VIEC");

                entity.HasOne(d => d.MaBsNavigation)
                    .WithMany(p => p.LichLamViecs)
                    .HasForeignKey(d => d.MaBs)
                    .HasConstraintName("FK__LICH_LAM___MA_BS__4F7CD00D");
            });

            modelBuilder.Entity<QuanLy>(entity =>
            {
                entity.HasKey(e => e.MaQl)
                    .HasName("PK__QUAN_LY__53E6F167EF9E0FF4");

                entity.ToTable("QUAN_LY");

                entity.HasIndex(e => e.MaTk, "UQ__QUAN_LY__53E1C493A92ADAAF")
                    .IsUnique();

                entity.Property(e => e.MaQl).HasColumnName("MA_QL");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.MaTk).HasColumnName("MA_TK");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenQl)
                    .HasMaxLength(100)
                    .HasColumnName("TEN_QL");

                entity.HasOne(d => d.MaTkNavigation)
                    .WithOne(p => p.QuanLy)
                    .HasForeignKey<QuanLy>(d => d.MaTk)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__QUAN_LY__MA_TK__3B75D760");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK__TAI_KHOA__53E1C492B1E6CD17");

                entity.ToTable("TAI_KHOAN");

                entity.HasIndex(e => e.TenDangNhap, "UQ__TAI_KHOA__7C8CF1DF030CF4FB")
                    .IsUnique();

                entity.Property(e => e.MaTk).HasColumnName("MA_TK");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(255)
                    .HasColumnName("MAT_KHAU");

                entity.Property(e => e.Role).HasColumnName("ROLE");

                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(50)
                    .HasColumnName("TEN_DANG_NHAP");
            });

            modelBuilder.Entity<TinTuc>(entity =>
            {
                entity.HasKey(e => e.MaTinTuc)
                    .HasName("PK__TIN_TUC__3E4147BA3FCA380D");

                entity.ToTable("TIN_TUC");

                entity.Property(e => e.MaTinTuc).HasColumnName("MA_TIN_TUC");

                entity.Property(e => e.NgayDang)
                    .HasColumnType("date")
                    .HasColumnName("NGAY_DANG")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NoiDung)
                    .HasColumnType("text")
                    .HasColumnName("NOI_DUNG");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
