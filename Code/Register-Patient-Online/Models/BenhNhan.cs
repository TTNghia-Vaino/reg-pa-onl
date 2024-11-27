using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Register_Patient_Online.Models
{
    public partial class BenhNhan
    {
        public BenhNhan()
        {
            DangKyKhams = new HashSet<DangKyKham>();
        }
        [Display(Name = "Mã Bệnh Nhân")]
        public string MaBn { get; set; } = null!;
        [Display(Name = "Mã Tài Khoản")]
        public int? MaTk { get; set; }
        [Display(Name = "Họ Và Tên")]
        public string Hoten { get; set; } = null!;
        [Display(Name = "Số Điện Thoại")]
        public string? Sdt { get; set; }
      
        public string? Email { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string? DiaChi { get; set; }
        [Display(Name = "Căn Cước Công Dân")]
        public string? Cccd { get; set; }
        [Display(Name = "Mã Bảo Hiểm Y Tế")]
        public string? MaBhyt { get; set; }
        
        public virtual BaoHiemYTe? MaBhytNavigation { get; set; }
        public virtual TaiKhoan? MaTkNavigation { get; set; }
        public virtual ICollection<DangKyKham> DangKyKhams { get; set; }
    }
}
