using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class DangKyKham
    {
        public int MaDk { get; set; }
        public string MaBn { get; set; } = null!;
        public string MaBs { get; set; } = null!;
        public DateTime NgayDangKi { get; set; }
        public DateTime NgayDenKham { get; set; }
        public string? TrangThai { get; set; }

        public virtual BenhNhan MaBnNavigation { get; set; } = null!;
        public virtual BacSi MaBsNavigation { get; set; } = null!;
    }
}
