using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.DTOs.Paging
{
    public class BasePaging
    {
        public BasePaging()
        {
            PageId = 1;
            TakeEntity = 2;
        }
        public int PageId { get; set; }
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TakeEntity { get; set; } // dar har safe chan ta ra namayesh dahad
        public int SkipEntity { get; set; } //dar har safe chan ta ra azash rad shavad
    }
}
