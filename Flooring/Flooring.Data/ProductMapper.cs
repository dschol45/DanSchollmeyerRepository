using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class ProductMapper
    {
        public static Product ProductFromString(string row)
        {
            string[] fields = row.Split(',');
            Product result = new Product()
            {
                ProductType = fields[0],
                CostPerSquareFoot = decimal.Parse(fields[1]),
                LaborCostPerSquareFoot = decimal.Parse(fields[2]),
            };
            return result;
        }
    }
}
