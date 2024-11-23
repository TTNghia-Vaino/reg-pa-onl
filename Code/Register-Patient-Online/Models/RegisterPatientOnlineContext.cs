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

        public virtual DbSet<BaoHiemYTe> BaoHiemYTes { get; set; } = null!;
        public virtual DbSet<BenhNhan> BenhNhans { get; set; } = null!;
        public virtual DbSet<DangKyKham> DangKyKhams { get; set; } = null!;
        public virtual DbSet<KhoaKhamBenh> KhoaKhamBenhs { get; set; } = null!;
        public virtual DbSet<QuanLy> QuanLies { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public virtual DbSet<TinTuc> TinTucs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-CHLMU27\\HAO;Initial Catalog=RegisterPatientOnline;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaoHiemYTe>(entity =>
            {
                entity.HasKey(e => e.MaBhyt)
                    .HasName("PK__BAO_HIEM__6B0F5FC213FB3048");

                entity.ToTable("BAO_HIEM_Y_TE");

                entity.Property(e => e.MaBhyt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
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

                entity.Property(e => e.Ten)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TEN");
            });

            modelBuilder.Entity<BenhNhan>(entity =>
            {
                entity.HasKey(e => e.MaBn)
                    .HasName("PK__BENH_NHA__53E64ECEC8537158");

                entity.ToTable("BENH_NHAN");

                entity.HasIndex(e => e.MaTk, "UQ__BENH_NHA__53E1C493A7FDBA52")
                    .IsUnique();

                entity.HasIndex(e => e.MaBhyt, "UQ__BENH_NHA__6B0F5FC335533285")
                    .IsUnique();

                entity.Property(e => e.MaBn)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MA_BN");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CCCD");

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DIA_CHI");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HOTEN");

                entity.Property(e => e.MaBhyt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("MA_BHYT");

                entity.Property(e => e.MaTk).HasColumnName("MA_TK");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.HasOne(d => d.MaBhytNavigation)
                    .WithOne(p => p.BenhNhan)
                    .HasForeignKey<BenhNhan>(d => d.MaBhyt)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__BENH_NHAN__MA_BH__5535A963");

                entity.HasOne(d => d.MaTkNavigation)
                    .WithOne(p => p.BenhNhan)
                    .HasForeignKey<BenhNhan>(d => d.MaTk)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__BENH_NHAN__MA_TK__5441852A");
            });

            modelBuilder.Entity<DangKyKham>(entity =>
            {
                entity.HasKey(e => e.MaDk)
                    .HasName("PK__DANG_KY___53E15A865D63A21D");

                entity.ToTable("DANG_KY_KHAM");

                entity.Property(e => e.MaDk).HasColumnName("MA_DK");

                entity.Property(e => e.MaBn)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MA_BN");

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .HasColumnName("MA_KHOA");

                entity.Property(e => e.NgayDangKi)
                    .HasColumnType("date")
                    .HasColumnName("NGAY_DANG_KI");

                entity.Property(e => e.NgayDenKham)
                    .HasColumnType("date")
                    .HasColumnName("NGAY_DEN_KHAM");

                entity.Property(e => e.TrangThai)
                    .HasColumnType("text")
                    .HasColumnName("TRANG_THAI")
                    .HasDefaultValueSql("('CH? XÁC NH?N')");

                entity.HasOne(d => d.MaBnNavigation)
                    .WithMany(p => p.DangKyKhams)
                    .HasForeignKey(d => d.MaBn)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DANG_KY_K__MA_BN__5EBF139D");

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.DangKyKhams)
                    .HasForeignKey(d => d.MaKhoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DANG_KY_K__MA_KH__5DCAEF64");
            });

            modelBuilder.Entity<KhoaKhamBenh>(entity =>
            {
                entity.HasKey(e => e.MaKhoa)
                    .HasName("PK__KHOA_KHA__65A8F1627E055A85");

                entity.ToTable("KHOA_KHAM_BENH");

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .HasColumnName("MA_KHOA");

                entity.Property(e => e.TenKhoa)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TEN_KHOA");
            });

            modelBuilder.Entity<QuanLy>(entity =>
            {
                entity.HasKey(e => e.MaQl)
                    .HasName("PK__QUAN_LY__53E6F167B08897D2");

                entity.ToTable("QUAN_LY");

                entity.HasIndex(e => e.MaTk, "UQ__QUAN_LY__53E1C493326B923D")
                    .IsUnique();

                entity.Property(e => e.MaQl).HasColumnName("MA_QL");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.MaTk).HasColumnName("MA_TK");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenQl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TEN_QL");

                entity.HasOne(d => d.MaTkNavigation)
                    .WithOne(p => p.QuanLy)
                    .HasForeignKey<QuanLy>(d => d.MaTk)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__QUAN_LY__MA_TK__4D94879B");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK__TAI_KHOA__53E1C4926F1DB500");

                entity.ToTable("TAI_KHOAN");

                entity.HasIndex(e => e.TenDangNhap, "UQ__TAI_KHOA__7C8CF1DF3E732CE0")
                    .IsUnique();

                entity.Property(e => e.MaTk).HasColumnName("MA_TK");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MAT_KHAU");

                entity.Property(e => e.Role).HasColumnName("ROLE");

                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TEN_DANG_NHAP");
            });

            modelBuilder.Entity<TinTuc>(entity =>
            {
                entity.HasKey(e => e.MaTinTuc)
                    .HasName("PK__TIN_TUC__3E4147BA90F9A18A");

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
