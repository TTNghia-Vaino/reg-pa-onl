using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class BaoHiemYTe
    {
        public string MaBhyt { get; set; } = null!;
        public int GioiTinh { get; set; }
        public DateTime NgayBd { get; set; }
        public DateTime NgayHh { get; set; }
        public DateTime NgaySinh { get; set; }

        public virtual BenhNhan? BenhNhan { get; set; }
    }
}
