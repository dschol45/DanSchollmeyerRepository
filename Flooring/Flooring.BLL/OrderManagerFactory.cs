using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models.Interfaces;

namespace Flooring.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            ITaxRepo taxRepo;
            IProductRepo prodRepo;
            switch (mode)
            {
                case "Test":
                    taxRepo = new TaxTestRepo();
                    prodRepo = new ProductTestRepo();
                    return new OrderManager(new OrderTestRepo(taxRepo), taxRepo, prodRepo);
                case "Production":
                    taxRepo = new TaxListRepo();
                    prodRepo = new ProductListRepo();
                    return new OrderManager(new OrderFileRepo(taxRepo),taxRepo,prodRepo);
                    throw new Exception("Mode not supported. Check with IT!");
                default:
                    throw new Exception("Mode not supported. Check with IT!");
            }
        }
    }
}
