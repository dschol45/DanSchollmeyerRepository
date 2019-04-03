using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.ViewModels
{
    public class SalesReportData
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalVehicles { get; set; }
    }
}