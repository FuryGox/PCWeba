using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using System.Web.Services;
using System.Web.Script.Services;

namespace PCWeb.Models
{
    public class Common
    {
        public static List<Chitietdonhang> GetChitietdonhangs() {
            return new DbContext("name=QLdienthoaiEntities1").Set<Chitietdonhang>().ToList();
        }
        public static List<Donhang> GetDonhangs()
        {
            return new DbContext("name=QLdienthoaiEntities1").Set<Donhang>().ToList();
        }
        public static List<Hangsanxuat> GetHangsanxuat()
        {
            return new DbContext("name=QLdienthoaiEntities1").Set<Hangsanxuat>().ToList();
        }
        public static List<Hedieuhanh> GetHedieuhanhs()
        {
            return new DbContext("name=QLdienthoaiEntities1").Set<Hedieuhanh>().ToList();
        }
        public static List<Nguoidung> GetNguoidung()
        {
            return new DbContext("name=QLdienthoaiEntities1").Set<Nguoidung>().ToList();
        }
        public static List<PhanQuyen> GetPhanQuyens()
        {
            return new DbContext("name=QLdienthoaiEntities1").Set<PhanQuyen>().ToList();
        }
        public static List<Sanpham> GetSanphams()
        {
            return new DbContext("name=QLdienthoaiEntities1").Set<Sanpham>().ToList();
        }
        
        public static List<Sanpham> GetPageSP(int n)
        {
            List<Sanpham> sp = new List<Sanpham>();
            QLdienthoaiEntities1 db= new QLdienthoaiEntities1();
            sp = db.Sanphams.OrderBy(m => m.Masp).Take(n).ToList<Sanpham>();
            return sp;
        }
        public static int TongSanPham()
        {
            int k = 0;
            foreach(Sanpham i in GetSanphams())
            {
                k++;
            }
            return k;
        }
        public static int TongNguoiDung()
        {
            int k = 0;
            foreach(Nguoidung i in GetNguoidung())
            {
                k++;
            }
            return k;
        }
        public static int TongDonHang()
        {
            int k = 0;
            foreach(Donhang i in GetDonhangs())
            {
                k++;
            }
            return k;
        }
        public static int HetHang()
        {
            int k = 0;
            foreach(Sanpham i in GetSanphams().Where(m => m.Soluong == 0))
            {
                k++;
            }
            return k;
        }
        public static List<AllDonHang> GetToanDonHang()
        {
            QLdienthoaiEntities1 db = new QLdienthoaiEntities1();
            List<AllDonHang> d = new List<AllDonHang>();
            var temp = (from E1 in db.Donhangs
                        join E2 in db.Chitietdonhangs on E1.Madon equals E2.Madon
                        select new AllDonHang
                        {
                            maDon = E1.Madon,
                            maSP = E2.Masp,
                            soLuong = E2.Soluong,
                            ngayDat = (DateTime)E1.Ngaydat,
                            TinhTrang = E1.Tinhtrang,
                            maNguoiDung = E1.MaNguoidung,
                            donGia = E2.Dongia,
                            ThanhTien = E2.Thanhtien

                        });
            d = temp.ToList<AllDonHang>();
            return d;
        }
        
        public decimal GetLoiNhuanThangNam(int i,int y)
        {
            decimal loiNhuan = 0;
            try {
                loiNhuan = GetToanDonHang().Where(m => m.ngayDat.Month == i).Where(m => m.ngayDat.Year == y).Sum(m => m.ThanhTien).Value;
            }
            catch (Exception ex)
            {
                loiNhuan = 0;
            }
            
            return loiNhuan;
        }
        
    }
}