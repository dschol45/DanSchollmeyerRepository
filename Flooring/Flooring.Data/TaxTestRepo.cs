using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Interfaces;

namespace Flooring.Data
{
    public class TaxTestRepo : ITaxRepo
    {
        private static Tax oh = new Tax
        {
            StateAbbreviation = "OH",
            StateName = "Ohio",
            TaxRate = 6.25m,
        };
        private static Tax pa = new Tax
        {
            StateAbbreviation = "PA",
            StateName = "Pennsylvania",
            TaxRate = 6.75m,
        };
        private static Tax mi = new Tax
        {
            StateAbbreviation = "MI",
            StateName = "Michigan",
            TaxRate = 5.75m,
        };
        private static Tax indi = new Tax
        {
            StateAbbreviation = "IN",
            StateName = "Indiana",
            TaxRate = 6.00m,
        };

        private static List<Tax> taxes = new List<Tax>
        {
            oh,
            pa,
            indi,
            mi
        };

        public List<Tax> Load()
        {
            return taxes;
        }
    }
}
