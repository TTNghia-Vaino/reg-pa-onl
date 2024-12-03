using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class TaiKhoan
    {
        public int MaTk { get; set; }
        public string TenDangNhap { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public int Role { get; set; }

        public virtual BacSi? BacSi { get; set; }
        public virtual BenhNhan? BenhNhan { get; set; }
        public virtual QuanLy? QuanLy { get; set; }
    }
}
