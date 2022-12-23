using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCWeb.Models
{
    public class paging
    {
        public int Total { get; set; }
        public int ActivePage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int PageStart { get; set; }
        public int PageEnd { get; set; }

        public paging() { }
        public paging(int total,int page, int pageSize = 10)
        {
            int totalpage =(int)Math.Ceiling((decimal)total/(decimal)pageSize);
            int activePage = page;

            int startPage = activePage - 2;
            int endPage = activePage + 3;
            if(startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if(endPage > totalpage) { 
            
                endPage= totalpage;
                if(endPage >10)
                {
                    startPage = endPage - 5;
                }
            }

        }
    }
}