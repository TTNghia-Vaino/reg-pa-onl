using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class KhoaKhamBenh
    {
        public KhoaKhamBenh()
        {
            DangKyKhams = new HashSet<DangKyKham>();
        }

        public string MaKhoa { get; set; } = null!;
        public string TenKhoa { get; set; } = null!;

        public virtual ICollection<DangKyKham> DangKyKhams { get; set; }
    }
}
