using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Responses;

namespace Flooring.Models.Interfaces
{
    public interface IRepo
    {
        int nextOrderNumber(string date);
        
        //create order
        void Create(OrderResponse response);
        //read all orders
        List<Order> Load(string date);
        //update order
        void Update(OrderResponse response);
        //delete order
        void Delete(OrderResponse response);
        
    }
}