using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;

namespace Flooring.UI.WorkFlows
{
    public class RemoveOrderWorkflow
    {
        IConsoleIO io = new ConsoleIO();
        DisplayOrdersResponse displayResponse = new DisplayOrdersResponse();
        DateTime dateTime = new DateTime();
        OrderManager manager = OrderManagerFactory.Create();
        OrderResponse orderResponse = new OrderResponse();
        string dateString = "";

        public void Execute()
        {
            bool isValidDate = false;

            Console.Clear();
            while (isValidDate == false)
            {
                dateString = io.GetDateFromUser("Please enter date of file to load.");
                dateTime = DateTime.ParseExact(dateString, "MMddyyyy", CultureInfo.GetCultureInfo("en-us"));
                displayResponse = manager.DisplayOrders(dateString);


                if (displayResponse.Success == true)
                {
                    foreach (var order in displayResponse.Orders)
                    {
                        io.DisplayOrder(order, dateTime);
                    }
                    isValidDate = true;
                }
            }

            int orderNumber = io.GetIntFromUser("Please enter order number to remove: ");

            bool isRemove = false;

            while (isRemove == false)
            {
                foreach (var order in displayResponse.Orders)
                {
                    if (orderNumber == order.OrderNumber)
                    {
                        RemoveOrder(order);
                        isRemove = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Order not found. Please enter valid order number.");
                    }
                }
            }
        }

        private void RemoveOrder(Order order)
        {
            Console.Clear();

            orderResponse.DateString = dateString;
            orderResponse.DateTime = dateTime;
            orderResponse.Order = order;
            orderResponse.Orders = displayResponse.Orders;

            io.DisplayOrder(order, dateTime);
            bool isSave = false;
            while (isSave == false)
            {
                string save = io.GetStringFromUser("\nAre you sure you want to Remove this order?\n(Y)es or (N)o:");
                switch (save.ToUpper())
                {
                    case "Y":
                    case "YES":
                        manager.RemoveOrder(orderResponse);
                        isSave = true;
                        break;
                    case "N":
                    case "NO":
                        isSave = true;
                        break;
                    default:
                        Console.WriteLine("Please select (Y)es or (N)o.");
                        break;
                }
            }
        }
    }
}
