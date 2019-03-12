using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.Data
{
    public class Student : IValidatableObject
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal GPA { get; set; }
        public Address Address { get; set; }
        public Major Major { get; set; }
        public List<Course> Courses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Major.MajorId == 0)
            {
                errors.Add(new ValidationResult("Please select a Major",
                    new[] { "MajorId" }));
            }
            
            if (GPA > 4 || GPA < 0)
            {
                errors.Add(new ValidationResult("GPA must be between 0 and 4.00", new[] { "GPA" }));
            }
            if (string.IsNullOrEmpty(LastName))
            {
                errors.Add(new ValidationResult("Please enter your Last Name", new[] { "LastName" }));
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                errors.Add(new ValidationResult("Please enter your First Name", new[] { "FirstName" }));
            }
            return errors;
        }
    }
}