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
    public class OrderFileRepo : IRepo
    {
        private static List<Order> orders;
        private static List<Tax> _taxes;
        private string FILENAME;

        public OrderFileRepo(ITaxRepo taxRepo)
        {
            _taxes = taxRepo.Load();
        }
        //Next Order Number (highest current order number +1)
        public int nextOrderNumber(string date)
        {

            orders = Load(date);
            int num = 0;
            foreach (Order order in orders)
            {
                if (order.OrderNumber > num)
                {
                    num = order.OrderNumber;
                }
            }
            num = num + 1;

            return num;
        }

        /// <summary>
        /// Load from a text file into the Order list
        /// </summary>
        public List<Order> Load(string date)
        {
            List<Order> results = new List<Order>();
            StreamReader sr = null;

            FILENAME = $"Orders_{date}.txt";

            try
            {
                //FILENAME = $"Orders_{date}.txt";
                sr = new StreamReader(FILENAME);
                sr.ReadLine();
                string row = "";
                while ((row = sr.ReadLine()) != null)
                {
                    Order o = OrderMapper.FromString(row);
                    o.OrderTax.StateName = TaxStateName(o.OrderTax.StateAbbreviation);
                    results.Add(o);
                }
                orders = results;
            }
            catch (FileNotFoundException)
            {
                //Console.WriteLine("File not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
            return results;
        }

        public void Create(OrderResponse response)
        {
            //check for file, load list into response, add new order to list
            //if (!File.Exists($"Orders_{response.DateString}.txt"))
            //{
            //    File.Create($"Orders_{response.DateString}.txt").Close();
            //}

            response.Orders = Load(response.DateString);
            response.Orders.Add(response.Order);
            SaveOrder(response);
        }
        
        public void Delete(OrderResponse editResponse)
        {
            foreach (var item in editResponse.Orders)
            {
                if (item.OrderNumber == editResponse.Order.OrderNumber)
                {
                    orders.Remove(item);
                    break;
                }
            }
            
            SaveOrder(editResponse);
        }

        public void Update(OrderResponse editResponse)
        {
            
            for (int i = 0; i < editResponse.Orders.Count; i++)
            {
                if (orders[i].OrderNumber != editResponse.Order.OrderNumber) continue;
                orders[i] = editResponse.Order;
                break;
            }
            SaveOrder(editResponse);
        }

        private void SaveOrder(OrderResponse response)
        {
            StreamWriter sw = null;
            
            
            try
            {
                sw = new StreamWriter($"Orders_{response.DateString}.txt");
                
                sw.Write("OrderNumber::CustomerName::State::TaxRate::ProductType::Area::CostPerSquareFoot::LaborCostPerSquareFoot::MaterialCost::LaborCost::Tax::Total");

                foreach (Order order in response.Orders)
                {
                    sw.Write("\n"+OrderMapper.ToString(order));
                    sw.Flush();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Something went wrong.");
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }

        public static string TaxStateName(string abb)
        {

            string name = "";
            foreach (var item in _taxes)
            {
                if (item.StateAbbreviation == abb)
                {
                    name = item.StateName;
                }
            }
            return name;
        }
    }
}
