using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;

namespace Flooring.Data
{
    public class OrderTestRepo : IRepo
    {
        private List<Tax> _taxes = new List<Tax>();

        private static Order _order = new Order
        {
            OrderNumber = 1,
            Area = 100,
            CustomerName = "Acme, Inc.",
            OrderProduct = new Product
            {
                ProductType = "Wood",
                CostPerSquareFoot = 5.15m,
                LaborCostPerSquareFoot = 4.75m,
            },
            OrderTax = new Tax
            {
                StateAbbreviation = "OH",
                StateName = "Ohio",
                TaxRate = 6.25m,
            },
        };
        List<Order> _orders = new List<Order>
        {
            _order
        };

        public OrderTestRepo(ITaxRepo taxRepo)
        {
            _taxes = taxRepo.Load();
        }

        public void Create(OrderResponse response)
        {
            _orders.Add(response.Order);
        }

        public void Delete(OrderResponse response)
        {
            _orders.Remove(response.Order);

        }

        public List<Order> Load(string date)
        {
            return _orders;
        }

        public int nextOrderNumber(string date)
        {
            int num = 0;
            foreach (Order order in _orders)
            {
                if (order.OrderNumber > num)
                {
                    num = order.OrderNumber;
                }
            }
            num = num + 1;
            return num;
        }

        public void Update(OrderResponse response)
        {
            _order = response.Order;
        }
    }
}
