using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Interfaces;

namespace Flooring.Data
{
    public class ProductTestRepo : IProductRepo
    {
        private static Product wood = new Product
        {
            ProductType = "Wood",
            CostPerSquareFoot = 5.15m,
            LaborCostPerSquareFoot = 4.75m
        };
        private static Product laminate = new Product
        {
            ProductType = "Laminate",
            CostPerSquareFoot = 1.75m,
            LaborCostPerSquareFoot = 2.10m
        };
        private static Product carpet = new Product
        {
            ProductType = "Carpet",
            CostPerSquareFoot = 2.25m,
            LaborCostPerSquareFoot = 2.10m
        };
        private static Product tile = new Product
        {
            ProductType = "Tile",
            CostPerSquareFoot = 3.50m,
            LaborCostPerSquareFoot = 4.15m
        };

        private static List<Product> products = new List<Product>
        {
            carpet,
            laminate,
            tile,
            wood
        };

        /// <summary>
        /// Load from a text file into the character list
        /// </summary>
        public List<Product> Load()
        {
            return products;
        }
    }
}
