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
    public class EditOrderWorkflow
    {
        OrderResponse editResponse = new OrderResponse();
        IConsoleIO io = new ConsoleIO();
        DisplayOrdersResponse displayResponse = new DisplayOrdersResponse();
        DateTime dateTime = new DateTime();
        OrderManager manager = OrderManagerFactory.Create();
        string userDate = "";

        public void Execute()
        {
            //get date, load and display orders on that date. Ask again if no orders found
            EditOrderDate();

            //get order number, verify it is an int
            int orderNumber = io.GetIntFromUser("Please enter order number to edit: ");

            bool isEdit = false;
            while (isEdit == false)
            {
                foreach (var order in displayResponse.Orders)
                {
                    //verify that order number matches an order in the list for that date
                    if (orderNumber == order.OrderNumber)
                    {
                        //set list, order, and dates to OrderResponse
                        editResponse.Orders = displayResponse.Orders;
                        editResponse.Order = order;
                        editResponse.DateString = userDate;
                        editResponse.DateTime = dateTime;
                        //run methods to get/validate/adjust order
                        EditOrder(order);
                        //yes/no to save, either exits, looped entry match (don't want to save/delete w/o confirmation)
                        SaveEdit();
                        isEdit = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Order not found. Please enter valid order number.");
                    }
                }
            }
        }

        private void EditOrderDate()
        {
            bool isValidDate = false;

            Console.Clear();
            while (isValidDate == false)
            {
                userDate = io.GetDateFromUser("Please enter date of file to load.");
                dateTime = DateTime.ParseExact(userDate, "MMddyyyy", CultureInfo.GetCultureInfo("en-us"));
                displayResponse = manager.DisplayOrders(userDate);


                if (displayResponse.Success == true)
                {
                    foreach (var order in displayResponse.Orders)
                    {
                        io.DisplayOrder(order, dateTime);
                    }
                    isValidDate = true;
                }
            }
        }

        public void EditOrder(Order order)
        {
            //display original order data (whole order)
            Console.Clear();
            io.DisplayOrder(editResponse);
            Console.WriteLine("\n");
            //display current name
            Console.WriteLine("Current Customer Name: " + order.CustomerName);
            string newName = EditCustomerName();
            //if blank no change
            if (newName != "")
            {
                order.CustomerName = newName;
            }

            //display current abbreviation and state name and options for change
            Console.WriteLine($"\nOrder currently set in {order.OrderTax.StateAbbreviation} / {order.OrderTax.StateName}.");
            string oldState = order.OrderTax.StateAbbreviation;
            string newState = EditState();
            //if blank no change
            if (newState != "")
            {
                order.OrderTax.StateAbbreviation = newState;
            }

            //display current product type/costs
            Console.WriteLine("\nProduct type is currently " + order.OrderProduct.ProductType);
            Console.WriteLine($"Cost/Sq Ft : {order.OrderProduct.CostPerSquareFoot} \t Labor/Sq Ft : {order.OrderProduct.LaborCostPerSquareFoot}");
            string oldType = order.OrderProduct.ProductType;
            string newType = EditProductType();

            //if blank, no change
            if (newType != "")
            {
                order.OrderProduct.ProductType = newType;
            }

            //display current area
            Console.WriteLine("\nArea is currently " + order.Area);
            decimal oldArea = order.Area;
            decimal newArea = EditArea();
            //if blank no change(blank entry outputs a decimal of -1)
            if (newArea != -1)
            {
                order.Area = newArea;
            }
            //clear to clean up console, display edited order
            Console.Clear();
            io.DisplayOrder(order, dateTime);
        }

        public string EditCustomerName()
        {
            string customerName = "";
            bool isValid = false;
            while (isValid == false)
            {
                Console.WriteLine();
                customerName = io.GetStringFromUser("Enter new name or Press Enter to skip:");
                if (customerName == "")
                {
                    return customerName;
                }
                if (io.ValidateName(customerName) == false)
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

        public string EditState()
        {
            string state = "";
            bool isValid = false;
            //display options
            io.DisplayStates(manager.TaxList());

            while (isValid == false)
            {
                //get state
                state = io.GetStringFromUser("Please enter new State Abbreviation or press Enter to skip: ");
                if (state == "")
                {
                    return state;
                }
                if (state.Length != 2)
                {
                    Console.WriteLine("State Abbreviation must be 2 characters long.");
                }
                else
                {
                    state = state.ToUpper();
                }

                TaxStateResponse taxResponse = manager.Tax(state);

                if (taxResponse.Success)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("The provided abbreviation does not match a state in our service region.");
                }
            }

            return state;
        }

        public string EditProductType()
        {
            bool isValid = false;
            ProductTypeResponse response = new ProductTypeResponse();
            string userType = "";
            //display options
            io.DisplayProducts(manager.ProductList());

            while (isValid == false) 
            {
                userType = io.GetStringFromUser("Please select product type or press enter to skip: ");
                if (userType=="")
                {
                    return userType;
                }
                //check if this product is an option on list
                response = manager.ProductType(userType);

                if (response.Success)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("The selected product type is not available.");
                }
            }

            return response.Product.ProductType;
        }

        public decimal EditArea()
        {
            bool isValid = false;
            decimal area = -1;

            while (isValid == false)
            {
                area = -1;
                string areaString = io.GetStringFromUser("Please enter new Area for the project or press Enter to skip: ");
                if (areaString == "")
                {
                    return area;
                }
                if (decimal.TryParse(areaString, out area))
                {
                    if (area >= 100)
                    {
                        isValid = true;
                    }
                }
                else
                {
                    Console.WriteLine("Area must be at least 100.");
                }
            }
            return area;
        }

        public void SaveEdit()
        {
            //yes save and exit, no just exit, forced decision loop
        bool isSave = false;
            while (isSave == false)
            {
                string save = io.GetStringFromUser("\nWould you like to save the changes to this order?\n(Y)es or (N)o:");
                switch (save.ToUpper())
                {
                    case "Y":
                    case "YES":
                        manager.EditOrder(editResponse);
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
