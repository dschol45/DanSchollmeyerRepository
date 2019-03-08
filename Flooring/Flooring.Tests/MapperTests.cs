using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models;
using NUnit.Framework;

namespace Flooring.Tests
{
    [TestFixture]
    class MapperTests
    {

        [Test]
        public void OrderToStringTests()
        {
            Product product = new Product
            {
                ProductType = "Wood",
                CostPerSquareFoot = 5.15m,
                LaborCostPerSquareFoot = 4.75m
            };
            Tax tax = new Tax
            {
                TaxRate = 6.25m,
                StateAbbreviation = "OH"
            };
            Order order = new Order()
            {
                OrderNumber = 1,
                CustomerName = "Wise",
                OrderTax = tax,
                OrderProduct = product,
                Area = 100m,
            };

            string actual = OrderMapper.ToString(order);
            string expected = "1::Wise::OH::6.25::Wood::100.00::5.15::4.75::515.00::475.00::61.88::1051.88";
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1::Wise::OH::6.25::Wood::100.00::5.15::4.75::515.00::475.00::61.88::1051.88", 1, "Wise", "OH", 6.25, "Wood", 100.00, 5.15, 4.75, 515.00, 475.00, 61.88, 1051.88)]
        public void OrderFromStringTest(string row, int id, string name, string abbreviation, decimal taxRate, string productType, decimal area, decimal materialCostPerSquareFoot, decimal laborCostPerSquareFoot, decimal materialCost, decimal laborCost, decimal taxAmt, decimal total)
        {
            Order actual = OrderMapper.FromString(row);

            Product product = new Product
            {
                ProductType = productType,
                CostPerSquareFoot = materialCostPerSquareFoot,
                LaborCostPerSquareFoot = laborCostPerSquareFoot,
            };
            Tax tax = new Tax
            {
                TaxRate = taxRate,
                StateAbbreviation = abbreviation
            };
            Order expected = new Order()
            {
                OrderNumber = id,
                CustomerName = name,
                OrderTax = tax,
                OrderProduct = product,
                Area = area,
            };

            Assert.AreEqual(expected.OrderTax.StateAbbreviation, actual.OrderTax.StateAbbreviation);
            Assert.AreEqual(expected.OrderTax.TaxRate, actual.OrderTax.TaxRate);

            Assert.AreEqual(expected.OrderProduct.ProductType, actual.OrderProduct.ProductType);
            Assert.AreEqual(expected.OrderProduct.CostPerSquareFoot, actual.OrderProduct.CostPerSquareFoot);
            Assert.AreEqual(expected.OrderProduct.LaborCostPerSquareFoot, actual.OrderProduct.LaborCostPerSquareFoot);

            Assert.AreEqual(expected.OrderNumber, actual.OrderNumber);
            Assert.AreEqual(expected.CustomerName, actual.CustomerName);
            Assert.AreEqual(expected.Area, actual.Area);
            Assert.AreEqual(expected.LaborCost, actual.LaborCost);
            Assert.AreEqual(expected.MaterialCost, actual.MaterialCost);
            Assert.AreEqual(expected.Tax, actual.Tax);
            Assert.AreEqual(expected.Total, actual.Total);

        }

        [TestCase("OH,Ohio,6.25", "OH", "Ohio", 6.25)]
        [TestCase("PA,Pennsylvania,6.75", "PA", "Pennsylvania", 6.75)]
        [TestCase("MI,Michigan,5.75", "MI", "Michigan", 5.75)]
        [TestCase("IN,Indiana,6.00", "IN", "Indiana", 6.00)]
        public void TaxFromStringTest(string row, string abbreviation, string name, decimal taxRate)
        {
            Tax actual = TaxMapper.FromString(row);

            Tax expected = new Tax
            {
                StateAbbreviation = abbreviation,
                StateName = name,
                TaxRate = taxRate,
            };


            Assert.AreEqual(expected.StateName, actual.StateName);
            Assert.AreEqual(expected.StateAbbreviation, actual.StateAbbreviation);
            Assert.AreEqual(expected.TaxRate, actual.TaxRate);
        }

        [TestCase("Carpet,2.25,2.10","Carpet", 2.25, 2.10)]
        [TestCase("Laminate,1.75,2.10","Laminate", 1.75, 2.10)]
        [TestCase("Tile,3.50,4.15","Tile", 3.50, 4.15)]
        [TestCase("Wood,5.15,4.75","Wood", 5.15, 4.75)]
        public void ProductFromString(string row, string type, decimal matCost, decimal laborCost)
        {
            Product actual = ProductMapper.ProductFromString(row);

            Product expected = new Product
            {
                ProductType = type,
                CostPerSquareFoot = matCost,
                LaborCostPerSquareFoot = laborCost,
            };


            Assert.AreEqual(expected.ProductType, actual.ProductType);
            Assert.AreEqual(expected.CostPerSquareFoot, actual.CostPerSquareFoot);
            Assert.AreEqual(expected.LaborCostPerSquareFoot, actual.LaborCostPerSquareFoot);
        }
    }
}
