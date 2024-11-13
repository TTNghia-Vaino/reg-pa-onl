using System;
using System.Collections.Generic;

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
        public string Hoten { get; set; } = null!;
        public string? Sdt { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? Cccd { get; set; }
        public string? MaBhyt { get; set; }

        public virtual BaoHiemYTe? MaBhytNavigation { get; set; }
        public virtual TaiKhoan? MaTkNavigation { get; set; }
        public virtual ICollection<DangKyKham> DangKyKhams { get; set; }
    }
}
