using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Models
{
    public class StockViewModel
    {
        public string ticker { set; get; }
        public string dateTime { set; get; }
        public string close { set; get; }
        public string volume { set; get; }
    }
}
