using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.EF;
using GuildCars.Models;

namespace GuildCars.ViewModels
{
    public class SaleInfoViewModel : IValidatableObject
    {
        public SaleInfo SaleInfo { get; set; }
        public Vehicle Vehicle { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }

        public void SetAllStateItems(IEnumerable<State> states)
        {
            List<SelectListItem> results = new List<SelectListItem>();
            foreach (var state in states)
            {
                results.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }

            States = results;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(SaleInfo.SaleName))
                errors.Add(new ValidationResult("Name is required", new[] { "SaleInfo.SaleName" }));
            if (string.IsNullOrEmpty(SaleInfo.SaleEmail) && string.IsNullOrEmpty(SaleInfo.SalePhone))
                errors.Add(new ValidationResult("Must enter either Phone or Email (or both)", new[] { "SaleInfo.SaleEmail","SaleInfo.SalePhone" }));
            if (!string.IsNullOrEmpty(SaleInfo.SaleEmail) && IsValidEmail(SaleInfo.SaleEmail) == false)
                errors.Add(new ValidationResult("Please enter a valid Email", new[] { "SaleInfo.SaleEmail" }));
            if (!string.IsNullOrEmpty(SaleInfo.SalePhone) && !Regex.Match(SaleInfo.SalePhone, @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$").Success)
                errors.Add(new ValidationResult("Please enter a valid Phone", new[] { "SaleInfo.SalePhone" }));
            if (string.IsNullOrEmpty(SaleInfo.SaleStreet1))
                errors.Add(new ValidationResult("Street 1 is required", new[] { "SaleInfo.SaleStreet1" }));
            if (string.IsNullOrEmpty(SaleInfo.SaleCity))
                errors.Add(new ValidationResult("City is required", new[] { "SaleInfo.SaleCity" }));
            if (string.IsNullOrEmpty(SaleInfo.SaleState))
                errors.Add(new ValidationResult("State is required", new[] { "SaleInfo.SaleState" }));
            //if (SaleInfo.SaleZip == 0m || SaleInfo.SaleZip.ToString().Length != 5 || !int.TryParse(SaleInfo.SaleZip.ToString(), out int zip))
            if (!Regex.Match(SaleInfo.SaleZip.ToString(), @"^\d{5}$").Success)
                errors.Add(new ValidationResult("Zip must be a 5 digit Integer", new[] { "SaleInfo.SaleZip" }));
            if (SaleInfo.SalePrice < (.95m * Vehicle.SalePrice) || SaleInfo.SalePrice > Vehicle.MSRP)
                errors.Add(new ValidationResult("Sale Price must be at least 95% of Vehicle Price and Less than MSRP", new[] { "SaleInfo.SalePrice" }));
            if (string.IsNullOrEmpty(SaleInfo.SaleType))
                errors.Add(new ValidationResult("Type is required", new[] { "SaleInfo.SaleType" }));

            return errors;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
