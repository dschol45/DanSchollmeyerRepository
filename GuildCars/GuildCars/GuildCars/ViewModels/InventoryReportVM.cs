using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuildCars.Data.EF;

namespace GuildCars.ViewModels
{
    public class InventoryReportVM
    {
        public IEnumerable<InventoryReportData> UsedInventory { get; set; }
        public IEnumerable<InventoryReportData> NewInventory { get; set; }
    }
}