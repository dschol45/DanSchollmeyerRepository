using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;
using System.Globalization;

namespace GuildCars.Data.Repo.Mock
{
    public class SaleInfoMockRepo : ISaleInfoRepo
    {
        private static List<SaleInfo> _sales;

        public SaleInfoMockRepo()
        {
            _sales = new List<SaleInfo>
           {
                new SaleInfo{ SaleId = 0, UserId = "b6e84ac7-294e-45c6-a716-5b67ee8dc52c", VehicleId = 3, SaleDate = new DateTime(2020,1,1), SaleName="Random Dude", SalePhone="1234567890", SaleEmail="dude@random.com", SaleStreet1="1234 Main St.", SaleStreet2="", SaleCity="Minneapolis", SaleState="MN", SaleZip=55112, SalePrice = 25000},
                new SaleInfo{ SaleId = 1, UserId = "b6e84ac7-294e-45c6-a716-5b67ee8dc52c", VehicleId = 4, SaleDate = new DateTime(2020,2,2), SaleName="Random Dude", SalePhone="1234567890", SaleEmail="dude@random.com", SaleStreet1="1234 Main St.", SaleStreet2="", SaleCity="Minneapolis", SaleState="MN", SaleZip=55112, SalePrice = 25000}
            };
        }

        public void Create(SaleInfo saleInfo)
        {
            saleInfo.SaleDate = DateTime.Today;

            if (_sales.Count == 0)
            {
                saleInfo.SaleId = 0;
            }
            else
            {
                saleInfo.SaleId = _sales.Max(m => m.SaleId) + 1;
            }

            _sales.Add(saleInfo);
        }

        public IEnumerable<SaleInfo> GetAll()
        {
            return _sales;
        }

        public SaleInfo GetById(int id)
        {
            return _sales.FirstOrDefault(s => s.SaleId == id);
        }

        public IEnumerable<SaleInfo> GetBySearch(string user, string minDate, string maxDate)
        {
            List<SaleInfo> sales;
            List<SaleInfo> results = new List<SaleInfo>();
            if (user != "All")
            {
                sales = GetAll().Where(s => s.UserId == user).ToList();
            }
            else
            {
                sales = GetAll().ToList();
            }

            DateTime.TryParseExact(minDate, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime min);
            DateTime.TryParseExact(maxDate, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime max);
            if (min > max)
            {
                results = new List<SaleInfo>();
            }
            else
            {
                foreach (var sale in sales)
                {
                    if (sale.SaleDate >= min && sale.SaleDate <= max)
                    {
                        results.Add(sale);
                    }
                }
            }
            return results;
        }
    }
}
