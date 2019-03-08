using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Responses
{
    public class OrderResponse : Response
    {
        public List<Order> Orders { get; set; }
        public Order Order { get; set; }
        public string DateString { get; set; }
        public DateTime DateTime { get; set; }
    }
}
