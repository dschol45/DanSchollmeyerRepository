using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Models;
using Flooring.Models.Responses;
using NUnit.Framework;

namespace Flooring.Tests
{
    [TestFixture]
    public class OrderManagerTests
    {
        OrderManager manager = OrderManagerFactory.Create();

        [TestCase("OH",true)]
        [TestCase("PA", true)]
        [TestCase("MI", true)]
        [TestCase("IN", true)]
        [TestCase("MN", false)]

        public void TaxTest(string state, bool expected)
        {
            TaxStateResponse actual = new TaxStateResponse();
            List<Tax> taxes = manager.TaxList();

            actual = manager.Tax(state);

            Assert.AreEqual(expected, actual.Success);

        }

        [TestCase("Wood",true)]
        [TestCase("Tile", true)]
        [TestCase("Laminate", true)]
        [TestCase("carpet", true)]
        [TestCase("wod", false)]
        public void ProductTypeTest(string type, bool expected)
        {
            ProductTypeResponse actual = new ProductTypeResponse();
            List<Product> products = manager.ProductList();

            actual = manager.ProductType(type);

            Assert.AreEqual(expected, actual.Success);
        }

        [TestCase("06012013",2)]
        public void NextOrderNumberTest(string date,int expected)
        {
            NextOrderNumberResponse actual = manager.NextOrderNumber(date);
            Assert.AreEqual(expected, actual.OrderNumber);
        }

        [TestCase("06012013",true)]
        public void DisplayOrderTest(string date, bool expected)
        {
            DisplayOrdersResponse actual = manager.DisplayOrders(date);
            Assert.AreEqual(expected, actual.Success);
        }
    }
}
