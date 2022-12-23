using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PCWeb.Models;
namespace PCWeb.Areas.Private.Controllers
{
    public class ViewSPController : Controller
    {
        private static bool isEdit = false;
        public static int page = 0;
        QLdienthoaiEntities1 db = new QLdienthoaiEntities1();

        

        // GET: Private/View
        [HttpGet]
        public ActionResult Index(ViewDataDictionary viewData)
        {
            List<Sanpham> l = new QLdienthoaiEntities1().Sanphams.OrderBy(x => x.Masp).ToList<Sanpham>();
            ViewData["DsSanpham"] = l;
            IEnumerable<Hangsanxuat> h = db.Hangsanxuats.AsEnumerable<Hangsanxuat>();
            ViewData["DsHSX"] = h;

            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Anhbia/"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return RedirectToAction("Index","ViewSP");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult Index(Sanpham x) {
            
            if(!isEdit) db.Sanphams.Add(x);
            else
            {
                //Submit if isEdit
                Sanpham y = db.Sanphams.Find(x.Masp);
                y.Soluong = x.Soluong;
                y.Tensp = x.Tensp;
                y.Giatien = x.Giatien;
                y.Mahang = x.Mahang;
                y.Anhbia = x.Anhbia;
                isEdit = false;
                
            }
            db.SaveChanges();
            //update data
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            List<Sanpham> l = db.Sanphams.OrderBy(m => m.Masp).ToList<Sanpham>();
            ViewData["DsSanpham"] = l;
            return View();
        }
        

        [HttpPost]
        public ActionResult delete(String x)
        {
            int id = Convert.ToInt32(x);
            try
            {
                Sanpham sp = db.Sanphams.Where(m => m.Masp == id).FirstOrDefault();
                db.Sanphams.Remove(sp);
                db.SaveChanges();
                ViewData["DsSanpham"] = db.Sanphams.ToList<Sanpham>();
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewData["DsSanpham"] = db.Sanphams.ToList<Sanpham>();
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult update(String maloai)
        {
            try
            {
                int x = Convert.ToInt32(maloai);
                isEdit= true;
                ViewData["DsSanpham"] = db.Sanphams.ToList<Sanpham>();
                return View("Index",db.Sanphams.Where(m => m.Masp == x).FirstOrDefault());
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        
    }
}