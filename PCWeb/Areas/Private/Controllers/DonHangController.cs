using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCWeb.Areas.Private.Controllers
{
    public class DonHangController : Controller
    {
        private static bool isEdit = false;
        public static int page = 0;
        QLdienthoaiEntities1 db = new QLdienthoaiEntities1();
        // GET: Private/View
        [HttpGet]
        public ActionResult Index()
        {
            List<AllDonHang> l = Common.GetToanDonHang();
            ViewData["DsDH"] = l;
            return View();
        }




        [HttpPost]
        public ActionResult Index(AllDonHang x)
        {

            if (!isEdit)
            {
                Donhang k = new Donhang();
                k.Madon = x.maDon;
                k.MaNguoidung = x.maNguoiDung;
                k.Ngaydat = DateTime.Now;
                db.Donhangs.Add(k);
                Chitietdonhang ct = new Chitietdonhang();
                ct.Madon = x.maDon;
                ct.Masp = x.maSP;
                ct.Soluong = x.soLuong;
                ct.Dongia = db.Sanphams.Where(m => m.Masp == x.maSP).Select(m => m.Giatien).First();
                ct.Thanhtien = x.soLuong * ct.Dongia;
                db.Chitietdonhangs.Add(ct);
            }
            else
            {
                //Submit if isEdit
                isEdit = false;
                int md = Convert.ToInt32(x.maDon);
                Donhang y = db.Donhangs.Find(md);
                y.MaNguoidung = x.maNguoiDung;
                y.Madon = x.maDon;
                y.Ngaydat = DateTime.Now;
                y.Tinhtrang = x.TinhTrang;
                Chitietdonhang ct = db.Chitietdonhangs.Where(m => m.Madon == md).First();
                ct.Masp = x.maSP;
                ct.Soluong = x.soLuong;
                ct.Dongia = db.Sanphams.Where(m => m.Masp == ct.Masp).Select(m => m.Giatien).First();
                ct.Thanhtien = ct.Dongia * ct.Soluong;
            }
            db.SaveChanges();
            //update data
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            List<AllDonHang> l = Common.GetToanDonHang();
            ViewData["DsDH"] = l;
            return View();
        }
        [HttpPost]
        public ActionResult delete(String x)
        {
            int id = Convert.ToInt32(x);
            try
            {
                Chitietdonhang a = db.Chitietdonhangs.Where(m => m.Madon == id).FirstOrDefault();
                Donhang b = db.Donhangs.Where(m => m.Madon == id).FirstOrDefault();
                List<AllDonHang> sp = Common.GetToanDonHang();
                db.Donhangs.Remove(b);
                db.Chitietdonhangs.Remove(a);
                db.SaveChanges();
                List<AllDonHang> l = Common.GetToanDonHang();
                ViewData["DsDH"] = l;
                return View("Index");
            }
            catch (Exception ex)
            {
                List<AllDonHang> l = Common.GetToanDonHang();
                ViewData["DsDH"] = l;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult update(String madon)
        {
            try
            {
                int x = Convert.ToInt32(madon);
                isEdit = true;
                List<AllDonHang> l = Common.GetToanDonHang();
                ViewData["DsDH"] = l;
                
                return View("Index", Common.GetToanDonHang().Where(m => m.maDon == x).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult commit(String madon)
        {
            try
            {
                int x = Convert.ToInt32(madon);
                List<AllDonHang> l = Common.GetToanDonHang();
                Donhang s =db.Donhangs.Find(x);
                s.Tinhtrang = 1;
                db.SaveChanges();
                ViewData["DsDH"] = l;
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewData["DsDH"] = Common.GetToanDonHang();
                return View("Index");
            }
        }
        

    }
}