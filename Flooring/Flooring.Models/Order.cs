using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    //class for Order: 
    //OrderNumber – int
    //CustomerName – string
    //State – string
    //TaxRate – decimal (get this from tax file)
    //ProductType – string (from product file)
    //Area – decimal
    //CostPerSquareFoot – decimal (from product file)
    //LaborCostPerSquareFoot – decimal (from product file)
    //MaterialCost – decimal (Area * CostPerSquareFoot)
    //LaborCost – decimal (Area * LaborCostPerSquareFoot)
    //Tax – decimal ((MaterialCost + LaborCost)*(TaxRate/100)
    //Total – decimal (MaterialCost + LaborCost + Tax)
    public class Order
    {
        //TODO: ask Amir when we should be rounding the numbers, only when displayed or round when saved to order. if to order, how?

        public int OrderNumber { get; set; }
        public Product OrderProduct { get; set; }
        public Tax OrderTax { get; set; }
        public string CustomerName { get; set; }
        public decimal Area { get; set; }
        public decimal MaterialCost {get => Area * OrderProduct.CostPerSquareFoot;}
        public decimal LaborCost {get => Area * OrderProduct.LaborCostPerSquareFoot;}
        public decimal Tax { get => (MaterialCost + LaborCost) * OrderTax.TaxRate / 100; }
        public decimal Total { get => MaterialCost + LaborCost + Tax; }
        //ProductType = OrderProduct.ProductType
        //CostPerSquareFoot = OrderProduct.CostPerSquareFoot
        //LaborCostPerSquareFoot = OrderProduct.LaborCostPerSquareFoot
        //TaxRate = OrderTax.TaxRate
        //State = OrderTax.StateName

    }
}
