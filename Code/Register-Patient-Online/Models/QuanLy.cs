using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class QuanLy
    {
        public int MaQl { get; set; }
        public int? MaTk { get; set; }
        public string TenQl { get; set; } = null!;
        public string? Sdt { get; set; }
        public string? Email { get; set; }

        public virtual TaiKhoan? MaTkNavigation { get; set; }
    }
}
