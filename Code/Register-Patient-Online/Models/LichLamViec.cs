using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class LichLamViec
    {
        public string MaBs { get; set; } = null!;
        public DateTime Ngay { get; set; }
        public string CaLamViec { get; set; } = null!;

        public virtual BacSi MaBsNavigation { get; set; } = null!;
    }
}
