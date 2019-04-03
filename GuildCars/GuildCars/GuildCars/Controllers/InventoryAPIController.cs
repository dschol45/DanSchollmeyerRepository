using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GuildCars.Data.EF;
using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.ViewModels;

namespace GuildCars.Controllers
{
    [EnableCors(origins: "*",headers: "*", methods: "*")]
    public class InventoryAPIController : ApiController
    {
        public static IVehicleRepo vRepo = Factory.GetVehicleRepo();
        public static IModelRepo modelRepo = Factory.GetModelRepo();
        public static IMakeRepo makeRepo = Factory.GetMakeRepo();

        [Route("api/inventory/search/{type}/{term}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(string type,string term,decimal minPrice,decimal maxPrice,int minYear,int maxYear)
        {
            IEnumerable<Vehicle> found = vRepo.Search(type, term, minPrice, maxPrice, minYear, maxYear);

            if (found==null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [Route("api/admin/modellist/{make}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult ModelList(int make)
        {
            IEnumerable<Model> found = modelRepo.GetAll().Where(m => m.MakeId == make);
            
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }
        
        [Route("api/reports/sales/{user}/{minDate}/{maxDate}")]
        public IHttpActionResult GetSales(string user, string minDate, string maxDate)
        {
            ISaleInfoRepo saleRepo = Factory.GetSaleInfoRepo();
            IUserRepo userRepo = Factory.GetUserRepo();

            IEnumerable<SaleInfo> sales = saleRepo.GetBySearch(user,minDate,maxDate);
            SalesReportVM vm = new SalesReportVM();
            vm.Users = userRepo.GetAll();
            List<SalesReportData> salesReportData = new List<SalesReportData>();

            foreach (var item in vm.Users)
            {
                SalesReportData data = new SalesReportData();

                data.UserName = item.FirstName + " " + item.LastName;
                data.UserId = item.UserId;

                decimal userSales = 0;
                int userVehicles = 0;
                foreach (var sale in sales)
                {
                    if (sale.UserId == item.UserId)
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
            IEnumerable<SalesReportData> results = salesReportData;
            if (salesReportData == null)
            {
                return NotFound();
            }

            return Ok(results);
        }
    }
    
}
