using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCWeb.Areas.Private.Controllers
{
    public class NguoiDungController : Controller
    {
        private static bool isEdit = false;
        public static int page = 0;
        QLdienthoaiEntities1 db = new QLdienthoaiEntities1();
        // GET: Private/View
        [HttpGet]
        public ActionResult Index()
        {
            List<Nguoidung> l = new QLdienthoaiEntities1().Nguoidungs.OrderBy(x => x.MaNguoiDung).ToList<Nguoidung>();
            ViewData["DsNguoidung"] = l;
            return View();
        }




        [HttpPost]
        public ActionResult Index(Nguoidung x)
        {

            if (!isEdit) db.Nguoidungs.Add(x);
            else
            {
                //Submit if isEdit
                isEdit = false;
                Nguoidung y = db.Nguoidungs.Find(x.MaNguoiDung);
                y.Dienthoai = x.Dienthoai;
                y.Hoten = x.Hoten;
                y.Email = x.Email;
                
                

            }
            db.SaveChanges();
            //update data
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            List<Nguoidung> l = db.Nguoidungs.OrderBy(m => m.MaNguoiDung).ToList<Nguoidung>();
            ViewData["DsNguoidung"] = l;
            return View();
        }
        [HttpPost]
        public ActionResult delete(String x)
        {
            int id = Convert.ToInt32(x);
            try
            {

                Nguoidung sp = db.Nguoidungs.Where(m => m.MaNguoiDung == id).FirstOrDefault();
                db.Nguoidungs.Remove(sp);
                db.SaveChanges();
                ViewData["DsNguoidung"] = db.Nguoidungs.ToList<Nguoidung>();
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewData["DsNguoidung"] = db.Nguoidungs.ToList<Nguoidung>();
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult update(String maloai)
        {
            try
            {
                int x = Convert.ToInt32(maloai);
                isEdit = true;
                ViewData["DsNguoidung"] = db.Nguoidungs.ToList<Nguoidung>();
                return View("Index", db.Nguoidungs.Where(m => m.MaNguoiDung == x).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
   }