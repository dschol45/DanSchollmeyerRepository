using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuildCars.Models;

namespace GuildCars.ViewModels
{
    public class SalesReportVM
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<SalesReportData> UserSales { get; set; }
    }
}