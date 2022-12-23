using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCWeb.Models
{
    public class AllDonHang
    {
        public int maDon { get; set; }
        public int maSP { get; set; }
        public int? soLuong { get; set; }
        public DateTime ngayDat { get; set; }
        public int? TinhTrang { get; set; }
        public int? maNguoiDung { get; set; }
        public decimal? donGia { get; set; }
        public decimal? ThanhTien { get; set;}

    }
}