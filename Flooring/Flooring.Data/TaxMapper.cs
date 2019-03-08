using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class TaxMapper
    {
        public static Tax FromString(string row)
        {
            string[] fields = row.Split(',');
            Tax result = new Tax()
            {
                StateAbbreviation = fields[0],
                StateName = fields[1],
                TaxRate = decimal.Parse(fields[2]),
            };
            return result;
        }
    }
}
