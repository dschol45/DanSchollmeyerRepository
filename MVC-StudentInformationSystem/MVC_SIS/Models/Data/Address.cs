using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Exercises.Models.Repositories;

namespace Exercises.Models.Data
{
    public class Address : IValidatableObject
    {
        public int AddressId { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string PostalCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (PostalCode != null)
            {
                int zip = 0;
                int.TryParse(PostalCode, out zip);
                if (zip < 1)
                {
                    errors.Add(new ValidationResult("PostalCode cannot be Negative or Zero!", new[] { "PostalCode" }));
                }
                if (zip > 99999)
                {
                    errors.Add(new ValidationResult("Zip Code cannot be greater than 99999!", new[] { "PostalCode" }));
                }
            }
            return errors;
        }
    }
}