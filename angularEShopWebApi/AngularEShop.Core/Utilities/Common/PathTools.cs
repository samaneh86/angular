using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.Utilities.Common
{
   public class PathTools
    {
        public static string Domain = "https://localhost:44336";
       public static string productImagePath = "/Images/products/origin/";
        public static string productImageServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/products/origin/");
    }
}
