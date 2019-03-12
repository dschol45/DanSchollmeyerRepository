using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Exercises.Models.Repositories;

namespace Exercises.Models.Data
{
    public class State : IValidatableObject
    {
        public string StateAbbreviation { get; set; }
        public string StateName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(StateAbbreviation))
            {
                errors.Add(new ValidationResult("Please enter State Abbreviation.",
                    new[] { "StateAbbreviation" }));
            }

            return errors;
        }


    }

}
