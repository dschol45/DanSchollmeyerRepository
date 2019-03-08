using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;

namespace Flooring.UI.WorkFlows
{
    public class DisplayOrdersWorkflow
    {
        IConsoleIO io = new ConsoleIO();
        string userDate = "";

        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();

            //****display available files to load****
            io.DisplayFiles(manager.FileList());
            //get string, verify date range, get back formatted string
            userDate = io.GetDateFromUser("Please enter date of file to load.");
            DisplayOrdersResponse response = manager.DisplayOrders(userDate);
            DateTime dateTime = DateTime.ParseExact(userDate, "MMddyyyy", CultureInfo.GetCultureInfo("en-us"));
            //if order found on this date, display order(s)
            if (response.Success == true)
            {
                foreach (var order in response.Orders)
                {
                    io.DisplayOrder(order, dateTime);
                }
            }

            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
        }
    }
}
