using System;
using System.Collections.Generic;

namespace Register_Patient_Online.Models
{
    public partial class TinTuc
    {
        public int MaTinTuc { get; set; }
        public string? NoiDung { get; set; }


        public DateTime? NgayDang { get; set; }
    }
}
