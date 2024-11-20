using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class DangKyKham
    {
        public int MaDk { get; set; }
        public string? MaBn { get; set; }
        
        public DateTime NgayDangKi { get; set; }
        public DateTime NgayDenKham { get; set; }
        public string MaKhoa { get; set; } = null!;
        public string? TrangThai { get; set; }

        public virtual BenhNhan? MaBnNavigation { get; set; }
        public virtual KhoaKhamBenh MaKhoaNavigation { get; set; } = null!;
    }
}
