using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.UI.WorkFlows;

namespace Flooring.UI
{
    public class Menu

    {

        public void MainMenu()
        {
            //Main menu, to include Display orders, add order, edit order, remove order, quit
            while (true)
            {
                Console.WriteLine("*************************************************************************************");
                Console.WriteLine("*\tFlooring Program ");
                Console.WriteLine("*");
                Console.WriteLine("* 1. Display Orders");
                Console.WriteLine("* 2. Add an Order");
                Console.WriteLine("* 3. Edit an Order");
                Console.WriteLine("* 4. Remove an Order");
                Console.WriteLine("* 5. Quit");
                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "1":
                        DisplayOrdersWorkflow displayOrders = new DisplayOrdersWorkflow();
                        displayOrders.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow addOrder = new AddOrderWorkflow();
                        addOrder.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow editOrder = new EditOrderWorkflow();
                        editOrder.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkflow removeOrder = new RemoveOrderWorkflow();
                        removeOrder.Execute();
                        break;
                    case "5":
                    case "Q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Entry");
                        break;
                }
            }
        }

        

    }
}
