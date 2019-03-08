using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Interfaces;

namespace Flooring.Data
{
    public class OrderMapper
    {

        public static string ToString(Order order)
        {
            // OrderNumber, CustomerName, State, TaxRate, ProductType, Area, CostPerSquareFoot,
            // LaborCostPerSquareFoot, MaterialCost, LaborCost, Tax, Total
            string format = "0.00";
            return $"{order.OrderNumber.ToString()}::{order.CustomerName}::{order.OrderTax.StateAbbreviation}::{order.OrderTax.TaxRate.ToString(format)}::{order.OrderProduct.ProductType}::{order.Area.ToString(format)}::{order.OrderProduct.CostPerSquareFoot.ToString(format)}::{order.OrderProduct.LaborCostPerSquareFoot.ToString(format)}::{order.MaterialCost.ToString(format)}::{order.LaborCost.ToString(format)}::{order.Tax.ToString(format)}::{order.Total.ToString(format)}";
        }


        public static Order FromString(string row)
        {
            string[] fields = row.Split(new string[] { "::" }, StringSplitOptions.None);

            Product product = new Product
            {
                ProductType = fields[4],
                CostPerSquareFoot = decimal.Parse(fields[6]),
                LaborCostPerSquareFoot = decimal.Parse(fields[7])
            };
            Tax tax = new Tax
            {
                StateAbbreviation = fields[2],
                TaxRate = decimal.Parse(fields[3]),
            };

            Order result = new Order()
            {
                OrderNumber = int.Parse(fields[0]),
                CustomerName = fields[1],
                OrderTax = tax,
                OrderProduct = product,
                Area = decimal.Parse(fields[5]),
            };
            return result;
        }

        
    }
}