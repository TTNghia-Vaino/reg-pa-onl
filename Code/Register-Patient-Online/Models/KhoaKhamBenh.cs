using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class KhoaKhamBenh
    {
        public KhoaKhamBenh()
        {
            BacSis = new HashSet<BacSi>();
        }

        public string MaKhoa { get; set; } = null!;
        public string TenKhoa { get; set; } = null!;

        public virtual ICollection<BacSi> BacSis { get; set; }
    }
}
