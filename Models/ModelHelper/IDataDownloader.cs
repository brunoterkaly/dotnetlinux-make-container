using AspNetCoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Controllers
{
    interface IDataDownloader
    {
        List<StockViewModel>  GetData();
    }
}
