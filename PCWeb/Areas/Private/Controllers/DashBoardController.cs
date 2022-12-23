using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace PCWeb.Areas.Private.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: Private/DashBoard
        HttpClient httpClient = new HttpClient();

        [HttpGet]
        public ActionResult Index()
        {
            List<AllDonHang> saleData = Common.GetToanDonHang();
            
            
            decimal?[] t = new decimal?[15];
            for (int i = 1; i < 13; i++) {
                try
                {
                    t[i] = (saleData.Where(m => m.ngayDat.Year == DateTime.Now.Year).Where(m => m.ngayDat.Month == i).Sum(m => m.ThanhTien)) ?? (decimal?)0;
                }
                catch(Exception e)
                {
                    t[i] = 0;
                }
            }
            decimal?[] l = new decimal?[15];
            for (int i = 1; i < 13; i++)
            {
                try
                {
                    l[i] = (saleData.Where(m => m.ngayDat.Year == DateTime.Now.Year-1).Where(m => m.ngayDat.Month == i).Sum(m => m.ThanhTien)) ?? (decimal?)0;
                }
                catch (Exception e)
                {
                    l[i] = 0;
                }
            }
            ViewData["DSL"] = l;
            ViewData["DST"] = t;
            return View(saleData);
        }
        


    }
}