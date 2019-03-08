using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;

namespace Flooring.UI
{
    public class ConsoleIO : IConsoleIO
    {
        public string GetStringFromUser(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            return input;
        }

        public string GetDateFromUser(string prompt)
        {
            string format = "MMddyyyy";
            string result = "";
            bool isValid = false;
            DateTime date;
            while (isValid == false)
            {
                string dateString = GetStringFromUser(prompt);
                bool validDate = DateTime.TryParse(dateString, CultureInfo.GetCultureInfo("en-us"), DateTimeStyles.NoCurrentDateDefault, out date);
                if (date.Year > 2012)
                {
                    result = date.ToString(format);
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid date");
                }
            }

            return result;
        }

        public int GetIntFromUser(string prompt)
        {
            int result = 0;
            bool isValid = false;
            while (isValid == false)
            {
                string number = GetStringFromUser(prompt);
                if (int.TryParse(number, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("This is not integer.");
                }
            }
            return result;
        }

        public string GetNameFromUser()
        {
            string customerName = "";
            bool isValid = false;
            while (isValid == false)
            {
                customerName = GetStringFromUser("Please enter Customer Name: ");
                if (ValidateName(customerName) == false)
                {
                    Console.WriteLine($"An invalid character was entered. Names may only use Letters, numbers, comma, and period");
                }
                else
                {
                    isValid = true;
                }
            }
            return customerName;
        }

        
        //new method , take in string from user, output bool
        //similar to response set up just within one class
        public bool ValidateName(string name)
        {
            bool isValid = true;
            while (isValid == true)
            {
                string options = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789,. ";
                foreach (var item in name)
                {
                    if (!options.Contains(item))
                    {
                        isValid = false;
                    }
                }
                break;
            }
            return isValid;
        }




        public void DisplayOrder(OrderResponse response)
        {
            //string dateString = date.ToString("MM/dd/yyyy");
            Console.WriteLine("==============================");
            Console.WriteLine($"{response.Order.OrderNumber} | {response.DateString:MM/dd/yyyy}");//need to fix assigning date from file. new orders will have date this one does not?
            Console.WriteLine($"{response.Order.CustomerName}");
            Console.WriteLine($"{response.Order.OrderTax.StateName}");
            Console.WriteLine($"Product : {response.Order.OrderProduct.ProductType}");
            Console.WriteLine($"Materials : {response.Order.MaterialCost:c}");
            Console.WriteLine($"Labor : {response.Order.LaborCost:c}");
            Console.WriteLine($"Tax : {response.Order.Tax:c}");
            Console.WriteLine($"Total : {response.Order.Total:c}");
            Console.WriteLine($"==============================");
        }

        public void DisplayOrder(Order order, DateTime date)//overload for DisplayOrder when the prior method does not have a response to give it)
        {
            string dateString = date.ToString("MM/dd/yyyy");
            Console.WriteLine("==============================");
            Console.WriteLine($"{order.OrderNumber} | {dateString}");
            Console.WriteLine($"{order.CustomerName}");
            Console.WriteLine($"{order.OrderTax.StateName}");
            Console.WriteLine($"Product : {order.OrderProduct.ProductType}");
            Console.WriteLine($"Materials : {order.MaterialCost:c}");
            Console.WriteLine($"Labor : {order.LaborCost:c}");
            Console.WriteLine($"Tax : {order.Tax:c}");
            Console.WriteLine($"Total : {order.Total:c}");
            Console.WriteLine($"==============================");
        }

        public void DisplayProducts(List<Product> products)
        {
            foreach (var item in products)
            {
                Console.WriteLine("----------------------");
                Console.WriteLine($"{item.ProductType}:");
                Console.WriteLine($"Material Cost / sqft: {item.CostPerSquareFoot}");
                Console.WriteLine($"Labor Cost / sqft: {item.LaborCostPerSquareFoot}");
            }
            Console.WriteLine("----------------------");

        }

        public void DisplayStates(List<Tax> taxes)
        {
            Console.WriteLine("----------------------");
            foreach (var item in taxes)
            {
                Console.WriteLine($"{item.StateAbbreviation} - {item.StateName}");
            }
            Console.WriteLine("----------------------");

        }
        
        //TODO: add method to display available files/dates for lookup/edit/remove?
        public void DisplayFiles(List<string> files)
        {
            foreach (var item in files)
            {
                Console.WriteLine(item);
            }
        }

    }
}
