using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Data;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using System.IO;

namespace Flooring.BLL
{
    public class OrderManager
    {
        private IRepo _orderRepo;
        List<Tax> taxes = new List<Tax>();
        List<Product> products = new List<Product>();

        public OrderManager(IRepo repo,ITaxRepo taxRepo,IProductRepo prodRepo)
        {
            _orderRepo = repo;

            taxes = taxRepo.Load();
            products = prodRepo.Load();
        }

        public List<Tax> TaxList()
        {
            return taxes;
        }

        public List<Product> ProductList()
        {
            return products;
        }

        public TaxStateResponse Tax(string state)
        {
            TaxStateResponse response = new TaxStateResponse();
            foreach (var item in taxes) //TODO: replace with a while loop
            {
                if (state == item.StateAbbreviation)
                {
                    response.Message = "State Verified";
                    response.Success = true;
                    response.Tax = item;
                    return response;
                }
                else
                {
                    response.Message = "Not a valid state abbreviation.";
                    response.Success = false;
                }
            }
            return response;
        }

        public ProductTypeResponse ProductType(string type)
        {
            ProductTypeResponse response = new ProductTypeResponse();

            foreach (var item in products)
            {
                if (type.ToUpper() == item.ProductType.ToUpper())
                {
                    response.Message = "Product type available.";
                    response.Success = true;
                    response.Product = item;
                    return response;
                }
                else
                {
                    response.Message = "Not a valid product type.";
                    response.Success = false;
                }
            }
            return response;
        }

        public NextOrderNumberResponse NextOrderNumber(string date)
        {
            NextOrderNumberResponse response = new NextOrderNumberResponse();
            response.OrderNumber = _orderRepo.nextOrderNumber(date);
            return response;
        }

        public void AddOrder(OrderResponse response)
        {
            _orderRepo.Create(response);
        }

        public void EditOrder(OrderResponse response)
        {
            _orderRepo.Update(response);
        }

        public void RemoveOrder(OrderResponse response)
        {
            _orderRepo.Delete(response);        }

        public DisplayOrdersResponse DisplayOrders(string date)
        {
            DisplayOrdersResponse response = new DisplayOrdersResponse();

            response.Orders = _orderRepo.Load(date);

            if (response.Orders.Count == 0)
            {
                response.Success = false;
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        //TODO: maybe clean up the output on this, but working currently
        public List<string> FileList()
        {
            List<string> results = new List<string>();
            string path = Environment.CurrentDirectory;
            string[] files = Directory.GetFiles(path, "Orders_*.txt");
            foreach (var item in files)
            {
                results.Add(item.Substring(item.Length-12,8));
            }

            return results;
        }
    }
}
