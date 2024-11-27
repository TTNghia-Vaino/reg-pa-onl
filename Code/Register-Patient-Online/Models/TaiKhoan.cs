using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Register_Patient_Online.Models
{
    public partial class TaiKhoan
    {
        public int MaTk { get; set; }
        [Display(Name = "Tài Khoản")]
        public string TenDangNhap { get; set; } = null!;
        [Display(Name = "Mật Khẩu")]
        public string MatKhau { get; set; } = null!;
        [Display(Name = "Quyền")]
        public int Role { get; set; }
        
        public virtual BenhNhan? BenhNhan { get; set; }
        public virtual QuanLy? QuanLy { get; set; }
    }
}
