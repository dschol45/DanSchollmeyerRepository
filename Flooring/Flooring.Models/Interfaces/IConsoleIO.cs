using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models.Responses;

namespace Flooring.Models.Interfaces
{
    public interface IConsoleIO
    {
        string GetStringFromUser(string prompt);
        string GetDateFromUser(string prompt);
        int GetIntFromUser(string prompt);
        void DisplayOrder(OrderResponse response);
        string GetNameFromUser();
        bool ValidateName(string name);
        void DisplayOrder(Order order, DateTime date);
        void DisplayProducts(List<Product> products);
        void DisplayStates(List<Tax> taxes);
        void DisplayFiles(List<string> files);
    }
}
