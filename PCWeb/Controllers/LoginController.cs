using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCWeb.Models;
namespace PCWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Nguoidung x)
        {
            QLdienthoaiEntities1 db = new QLdienthoaiEntities1();
            var taikhoan = db.Nguoidungs.Where(m => m.Email == x.Email);
            if (taikhoan.Any())
            {
                if(taikhoan.Where(m => m.Matkhau == x.Matkhau).Any())
                {
                    if(taikhoan.Where(m => m.IDQuyen == 2).Any())
                    {
                        
                        return RedirectToAction("Index", "DashBoard", new { area = "Private" });
                    }
                    else
                    {
                        return Json(new { status = true, message = "Login Successfull!" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Password!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid Email!" });
            }
            
        }
        

        
    }
}