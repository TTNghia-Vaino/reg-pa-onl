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

        public string MaBn { get; set; } = null!;
        public int? MaTk { get; set; }

        [Display(Name = "Họ Tên")]
        public string Hoten { get; set; } = null!;


        [Display(Name = "Số Điện Thoại")]
        public string? Sdt { get; set; }
        public string? Email { get; set; }

        [Display(Name = "Địa Chỉ")]
        public string? DiaChi { get; set; }

        [Display(Name = "CCCD")]
        public string? Cccd { get; set; }
        public string? MaBhyt { get; set; }

        [Display(Name = "Mã BHYT")]
        public virtual BaoHiemYTe? MaBhytNavigation { get; set; }

        [Display(Name = "Tài Khoản")]
        public virtual TaiKhoan? MaTkNavigation { get; set; }
        public virtual ICollection<DangKyKham> DangKyKhams { get; set; }
        public virtual ICollection<LichSuKham> LichSuKhams { get; set; } = new HashSet<LichSuKham>();
    }
   

}
