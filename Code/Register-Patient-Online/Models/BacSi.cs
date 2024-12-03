using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class BacSi
    {
        public BacSi()
        {
            DangKyKhams = new HashSet<DangKyKham>();
            LichLamViecs = new HashSet<LichLamViec>();
        }

        public string MaBs { get; set; } = null!;
        public string Ten { get; set; } = null!;
        public int? MaTk { get; set; }
        public string MaKhoa { get; set; } = null!;

        public virtual KhoaKhamBenh MaKhoaNavigation { get; set; } = null!;
        public virtual TaiKhoan? MaTkNavigation { get; set; }
        public virtual ICollection<DangKyKham> DangKyKhams { get; set; }
        public virtual ICollection<LichLamViec> LichLamViecs { get; set; }
    }
}
