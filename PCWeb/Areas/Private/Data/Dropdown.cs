using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCWeb.Areas.Private.Data
{
    public static class Dropdown
    {
        public static IEnumerable<SelectListItem> GetAllHSXCategories()
        {   
            QLdienthoaiEntities1 d=new QLdienthoaiEntities1();
            
            return d.Hangsanxuats.Select(x => new SelectListItem { Text = x.Tenhang, Value = x.Mahang.ToString() });
        }
    }
}