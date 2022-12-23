using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCWeb.Areas.Private.Controllers
{
    public class HangSXController : Controller
    {
        private static bool isEdit = false;
        public static int page = 0;
        QLdienthoaiEntities1 db = new QLdienthoaiEntities1();
        // GET: Private/View
        [HttpGet]
        public ActionResult Index()
        {
            List<Hangsanxuat> l = new QLdienthoaiEntities1().Hangsanxuats.OrderBy(x => x.Mahang).ToList<Hangsanxuat>();
            ViewData["DsHangsanxuat"] = l;
            return View();
        }




        [HttpPost]
        public ActionResult Index(Hangsanxuat x)
        {

            if (!isEdit) db.Hangsanxuats.Add(x);
            else
            {
                //Submit if isEdit
                isEdit = false;
                Hangsanxuat y = db.Hangsanxuats.Find(x.Mahang);
                y.Tenhang = x.Tenhang;
            }
            db.SaveChanges();
            //update data
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            List<Hangsanxuat> l = db.Hangsanxuats.OrderBy(m => m.Mahang).ToList<Hangsanxuat>();
            ViewData["DsHangsanxuat"] = l;
            return View();
        }
        [HttpPost]
        public ActionResult delete(String x)
        {
            int id = Convert.ToInt32(x);
            try
            {

                Hangsanxuat sp = db.Hangsanxuats.Where(m => m.Mahang == id).FirstOrDefault();
                db.Hangsanxuats.Remove(sp);
                db.SaveChanges();
                ViewData["DsHangsanxuat"] = db.Hangsanxuats.ToList<Hangsanxuat>();
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewData["DsHangsanxuat"] = db.Hangsanxuats.ToList<Hangsanxuat>();
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
                ViewData["DsHangsanxuat"] = db.Hangsanxuats.ToList<Hangsanxuat>();
                return View("Index", db.Hangsanxuats.Where(m => m.Mahang == x).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}