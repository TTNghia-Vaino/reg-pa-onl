using System;
using System.ComponentModel.DataAnnotations;

namespace Register_Patient_Online.Models
{
    public partial class LichSuKham
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Mã Bệnh Nhân")]
        public string MaBn { get; set; } = null!;

        [Display(Name = "Ngày Khám")]
        public DateTime NgayKham { get; set; }

        [Display(Name = "Giờ Khám")]
        public TimeSpan GioKham { get; set; }

        [Display(Name = "Mô Tả Bệnh Án")]
        public string MoTaBenhAn { get; set; } = null!;
        public string? BacSi { get; set; }

        public virtual BenhNhan? BenhNhan { get; set; }

    }
}
