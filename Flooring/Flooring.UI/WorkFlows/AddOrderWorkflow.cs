using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.UI;
using Flooring.BLL;
using Flooring.Models;
using Flooring.Models.Interfaces;
using System.Globalization;
using Flooring.Models.Responses;

namespace Flooring.UI.WorkFlows
{
    public class AddOrderWorkflow
    {
        //declare interfaces and class level variables
        IConsoleIO io = new ConsoleIO();

        DateTime dateTime = new DateTime();
        Order order = new Order();
        OrderManager manager = OrderManagerFactory.Create();

        OrderResponse orderResponse = new OrderResponse();
        ProductTypeResponse productResponse = new ProductTypeResponse();
        TaxStateResponse taxResponse = new TaxStateResponse();
        NextOrderNumberResponse orderNumberResponse = new NextOrderNumberResponse();

        string dateString = "";
        string state = "";
        decimal area = 0;
        string customerName = "";

        public void Execute()
        {
            //method for each item we need, each item validated and looped before next item acquired
            Console.Clear();
            NewOrderDate();
            customerName = io.GetNameFromUser();
            NewOrderTax();
            NewOrderProduct();
            NewOrderArea();
            //assign acquired/validated values to correct order properties
            AssignOrderValues();
            //yes/no save & exit or exit, forced decision
            CommitNewOrder();
        }

        private void NewOrderDate()
        {
            bool isValidDate = false;
            while (isValidDate == false)
            {
                //get date, verify format and generate string of formatted date
                dateString = io.GetDateFromUser("Please enter date for order: ");
                //make sure the date string (as a DateTime) is after today
                dateTime = DateTime.ParseExact(dateString, "MMddyyyy", CultureInfo.GetCultureInfo("en-us"));

                if (dateTime < DateTime.Today)
                {
                    Console.WriteLine("Date must be after today.");
                }
                else
                {
                    isValidDate = true;
                }
            }
            //next order number, need date/load for this
            orderNumberResponse = manager.NextOrderNumber(dateString);
        }

        private void NewOrderTax()
        {
            bool isValidTax = false;

            while (isValidTax == false)
            {
                //display options
                io.DisplayStates(manager.TaxList());
                //get and verify string format for state abbreviation
                state = io.GetStringFromUser("Please enter State Abbreviation (OH format): ");
                if (state.Length != 2)
                {
                    Console.WriteLine("State Abbreviation must be 2 characters long.");
                }
                else
                {
                    state = state.ToUpper();
                }
                //verify state is on list
                taxResponse = manager.Tax(state);

                if (taxResponse.Success)
                {
                    isValidTax = true;
                }
                else
                {
                    Console.WriteLine("The provided abbreviation does not match a state in our service region.");
                }
            }
        }

        private void NewOrderProduct()
        {
            bool isValidProduct = false;
            while (isValidProduct == false) //add ignore case in validation
            {
                //display product options
                io.DisplayProducts(manager.ProductList());
                //get product type string and check list for match
                string userType = io.GetStringFromUser("Please select product type: ");
                productResponse = manager.ProductType(userType);

                if (productResponse.Success)
                {
                    isValidProduct = true;
                }
                else
                {
                    Console.WriteLine("The selected product type is not available.");
                }
            }
        }

        private void NewOrderArea()
        {
            bool isValidArea = false;
            while (isValidArea == false)
            {
                //get string(validate decimal.tryparse and >= 100)
                string areaString = io.GetStringFromUser("Please enter the Area size(in sq ft, Min 100) of the project: ");
                if (decimal.TryParse(areaString, out area))
                {
                    if (area >= 100)
                    {
                        isValidArea = true;
                    }

                }
                else
                {
                    Console.WriteLine("Invalid Entry. Area must be over 100.");
                }
            }
        }

        private void AssignOrderValues()
        {
            //assign acquired values to correct order (and response) properties
            order.OrderNumber = orderNumberResponse.OrderNumber;
            order.CustomerName = customerName;
            order.Area = area;
            order.OrderProduct = productResponse.Product;
            order.OrderTax = taxResponse.Tax;

            orderResponse.Order = order;
            orderResponse.DateString = dateString;
            orderResponse.DateTime = dateTime;
        }

        private void CommitNewOrder()
        {
            //display new order
            Console.Clear();
            io.DisplayOrder(order, dateTime);

            //yes/no forced decision loop to save/exit or exit
            bool isEnd = false;
            while (isEnd == false)
            {
                string save = io.GetStringFromUser("\nAre you sure you want to Create this order?\n(Y)es or (N)o:");
                switch (save.ToUpper())
                {
                    case "Y":
                    case "YES":
                        manager.AddOrder(orderResponse);
                        isEnd = true;
                        break;
                    case "N":
                    case "NO":
                        isEnd = true;
                        break;
                    default:
                        Console.WriteLine("Invalid answer.");
                        break;
                }
            }
        }
    }
}
