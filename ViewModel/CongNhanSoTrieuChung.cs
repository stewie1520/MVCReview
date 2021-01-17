using System;

using MVCReview.Models;

namespace MVCReview.ViewModel
{
    public class CongNhanSoTrieuChung
    {
        public int MaCongNhan { get; set; }
        public string TenCongNhan { get; set; }
        public bool GioiTinh { get; set; }
        public int NamSinh { get; set; }
        public string NuocVe { get; set; }
        public int MaDiemCachLy { get; set; }

        public int SoTrieuChung { get; set; }
    }
}