using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.EF;
using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.ViewModels;

namespace GuildCars.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportsController : Controller
    {
        IVehicleRepo vRepo = Factory.GetVehicleRepo();
        IModelRepo modelRepo = Factory.GetModelRepo();
        IMakeRepo makeRepo = Factory.GetMakeRepo();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            IEnumerable<Vehicle> allVehicles = vRepo.GetAll();
            foreach (var item in allVehicles)
            {
                item.Make = makeRepo.GetById(item.MakeId);
                item.Model = modelRepo.GetById(item.ModelId);
            }

            IEnumerable<Vehicle> newVehicles = allVehicles.Where(v => v.Type == "New");
            IEnumerable<Vehicle> usedVehicles = allVehicles.Where(v => v.Type == "Used");
            InventoryReportVM inventoryReportVM = new InventoryReportVM();

            inventoryReportVM.UsedInventory =  Inventory(usedVehicles);
            inventoryReportVM.NewInventory = Inventory(newVehicles);
            return View(inventoryReportVM);
        }

        private IEnumerable<InventoryReportData> Inventory(IEnumerable<Vehicle> vehicles)
        {
            var newDataQ =  from car in vehicles
                            group car by new { year = car.Year, model = car.Model.ModelName, make = car.Make.MakeName }
                            into g
                                select new InventoryReportData
                                {
                                    Year = g.Key.year,
                                    Make = g.Key.make,
                                    Model = g.Key.model,
                                    Count = g.Count(),
                                    StockValue = g.Sum(c => c.MSRP)
                                };

            return newDataQ;
        }

        public ActionResult Sales()
        {
            ISaleInfoRepo saleRepo = Factory.GetSaleInfoRepo();
            IUserRepo userRepo = Factory.GetUserRepo();

            IEnumerable<SaleInfo> sales = saleRepo.GetAll();
            SalesReportVM vm = new SalesReportVM();
            vm.Users = userRepo.GetAll();
            List<SalesReportData> salesReportData = new List<SalesReportData>();

            foreach (var user in vm.Users)
            {
                SalesReportData data = new SalesReportData();

                data.UserName = user.FirstName + " " + user.LastName;
                data.UserId = user.UserId;

                decimal userSales = 0;
                int userVehicles = 0;
                foreach (var sale in sales)
                {
                    if (sale.UserId == user.UserId)
                    {
                        userSales += sale.SalePrice;
                        userVehicles++;
                    }
                }

                data.TotalSales = userSales;
                data.TotalVehicles = userVehicles;

                if (data.TotalVehicles > 0)
                {
                    salesReportData.Add(data);
                }
            }

            vm.UserSales = salesReportData;

            return View(vm);
        }
    }
}