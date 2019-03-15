using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TipCalculator.Models
{
    public class Tip : IValidatableObject
    {
        public decimal Total { get; set; }
        public decimal? TipPercent { get; set; }
        public decimal TipAmount
        {
            get
            {
                if (decimal.TryParse(TipPercent.ToString(), out decimal TipPercentValue))
                {
                    return Total * TipPercentValue/100;
                }
                return 0;
            }
        }
        public decimal NewTotal => Total + TipAmount;
        public int Patrons { get; set; }
        public decimal Split => NewTotal / Patrons;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Total <= 0.01m)
            {
                errors.Add(new ValidationResult("Total must be at least $0.01", new[] { "Total" }));
            }

            if (TipPercent is decimal)
            {
                decimal percent = (decimal)TipPercent;
                if (percent < 0)
                {
                    errors.Add(new ValidationResult("Tip Percent cannot be negative!", new[] { "TipPercent" }));
                }
            }
            return errors;
        }
    }
}