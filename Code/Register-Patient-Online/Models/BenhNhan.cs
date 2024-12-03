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

        [Display(Name = "Full Name")]
        public string Hoten { get; set; } = null!;


        [Display(Name = "Phone Number")]
        public string? Sdt { get; set; }
        public string? Email { get; set; }

        [Display(Name = "Address")]
        public string? DiaChi { get; set; }

        [Display(Name = "CCCD")]
        public string? Cccd { get; set; }

        [Display(Name = "Insurance Code")]
        public string? MaBhyt { get; set; }
      
        public virtual BaoHiemYTe? MaBhytNavigation { get; set; }

        [Display(Name = "Account")]
        public virtual TaiKhoan? MaTkNavigation { get; set; }
        public virtual ICollection<DangKyKham> DangKyKhams { get; set; }

    }
   

}
