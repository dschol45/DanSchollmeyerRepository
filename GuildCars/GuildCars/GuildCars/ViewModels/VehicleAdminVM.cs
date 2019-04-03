using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.EF;

namespace GuildCars.ViewModels
{
    public class VehicleAdminVM : IValidatableObject
    {
        public Vehicle Vehicle { get; set; }
        public IEnumerable<SelectListItem> Makes { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> BodyStyles { get; set; }
        public IEnumerable<SelectListItem> Transmissions { get; set; }
        public IEnumerable<SelectListItem> Colors { get; set; }
        public IEnumerable<SelectListItem> Interiors { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase UploadedPic { get; set; }
        public string FileType { get; set; }

        public void SetAllListItems()
        {
            SetTypesListItems();
            SetBodyStylesListItems();
            SetTransmissionsListItems();
            SetColorListItems();
        }

        //public void SetMakesList(IEnumerable<Make> makeList)
        //{
        //    List<SelectListItem> makes = new List<SelectListItem>();
        //    foreach (var item in makeList)
        //    {
        //        makes.Add(new SelectListItem()
        //        {
        //            Value = item.MakeId.ToString(),
        //            Text = item.MakeName
        //        });
        //    }
            
        //}

        //public void SetModelsList(IEnumerable<Model> modelList)
        //{
        //    List<SelectListItem> models = new List<SelectListItem>();
        //    foreach (var item in modelList)
        //    {
        //        models.Add(new SelectListItem()
        //        {
        //            Value = item.ModelId.ToString(),
        //            Text = item.ModelName
        //        });
        //    }

        //}

        private void SetTypesListItems()
        {
            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(new SelectListItem()
            {
                Value = "New",
                Text = "New"
            });
            types.Add(new SelectListItem()
            {
                Value = "Used",
                Text = "Used"
            });
            Types = types;
        }

        private void SetBodyStylesListItems()
        {
            List<SelectListItem> styles = new List<SelectListItem>();
            styles.Add(new SelectListItem()
            {
                Value = "Car",
                Text = "Car"
            });
            styles.Add(new SelectListItem()
            {
                Value = "Truck",
                Text = "Truck"
            });
            styles.Add(new SelectListItem()
            {
                Value = "SUV",
                Text = "SUV"
            });
            styles.Add(new SelectListItem()
            {
                Value = "Van",
                Text = "Van"
            });

            BodyStyles = styles;
        }

        private void SetTransmissionsListItems()
        {
            List<SelectListItem> trans = new List<SelectListItem>();
            trans.Add(new SelectListItem()
            {
                Value = "Automatic",
                Text = "Automatic"
            });
            trans.Add(new SelectListItem()
            {
                Value = "Manual",
                Text = "Manual"
            });
            Transmissions = trans;
        }

        private void SetColorListItems()
        {
            List<SelectListItem> colors = new List<SelectListItem>();
            colors.Add(new SelectListItem()
            {
                Value = "Black",
                Text = "Black"
            });
            colors.Add(new SelectListItem()
            {
                Value = "White",
                Text = "White"
            });
            colors.Add(new SelectListItem()
            {
                Value = "Silver",
                Text = "Silver"
            });
            colors.Add(new SelectListItem()
            {
                Value = "Gold",
                Text = "Gold"
            });
            colors.Add(new SelectListItem()
            {
                Value = "Red",
                Text = "Red"
            });
            colors.Add(new SelectListItem()
            {
                Value = "Orange",
                Text = "Orange"
            }); colors.Add(new SelectListItem()
            {
                Value = "Yellow",
                Text = "Yellow"
            }); colors.Add(new SelectListItem()
            {
                Value = "Green",
                Text = "Green"
            }); colors.Add(new SelectListItem()
            {
                Value = "Blue",
                Text = "Blue"
            });
            colors.Add(new SelectListItem()
            {
                Value = "Purple",
                Text = "Purple"
            });

            Interiors = colors;
            Colors = colors;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Vehicle.MakeId < 1)
                errors.Add(new ValidationResult("Invalid Make", new[] { "Vehicle.MakeId" }));
            if (Vehicle.ModelId < 1)
                errors.Add(new ValidationResult("Invalid Model", new[] { "Vehicle.ModelId" }));
            if (Vehicle.Type != "New" && Vehicle.Type != "Used")
                errors.Add(new ValidationResult("Invalid Type", new[] { "Vehicle.Type" }));
            if (Vehicle.BodyStyle != "Car" && Vehicle.BodyStyle != "SUV" && Vehicle.BodyStyle != "Truck" && Vehicle.BodyStyle != "Van")
                errors.Add(new ValidationResult("Invalid Body Style", new[] { "Vehicle.BodyStyle" }));
            if (!Regex.Match(Vehicle.Year.ToString(), @"^[2][0](([0-1]\d)||([2][0])){1}$").Success)
                errors.Add(new ValidationResult("Year must be between 2000 and 2020", new[] { "Vehicle.Year" }));
            if (Vehicle.Transmission != "Automatic" && Vehicle.Transmission != "Manual")
                errors.Add(new ValidationResult("Transmission must be Automatic or Manual", new[] { "Vehicle.Transmission" }));
            if (Vehicle.Color == "")
                //check against list
                errors.Add(new ValidationResult("Color is required", new[] { "Vehicle.Color" }));
            if (Vehicle.Interior == "")
                //check against list
                errors.Add(new ValidationResult("Interior Color is required", new[] { "Vehicle.Interior" }));
            if (Vehicle.Mileage < 0)
                errors.Add(new ValidationResult("Mileage must be positive", new[] { "Vehicle.Mileage" }));
            if (Vehicle.Type == "New" && Vehicle.Mileage > 1000)
                errors.Add(new ValidationResult("Vehicles over 1000 miles cannot be sold as new", new[] { "Vehicle.Mileage" ,"Vehicle.Type"}));
            if (Vehicle.Type == "Used" && Vehicle.Mileage <= 1000)
                errors.Add(new ValidationResult("Vehicles under 1000 miles are sold as new", new[] { "Vehicle.Mileage","Vehicle.Type" }));
            if (Vehicle.VIN == null || !Regex.Match(Vehicle.VIN, @"^[0-9A-Z]{17}$").Success)
                errors.Add(new ValidationResult("VIN Must be 17 characters, and only contain Numbers and Letters", new[] { "Vehicle.VIN" }));
            if (Vehicle.MSRP < 0 )
                errors.Add(new ValidationResult("MSRP must be positive", new[] { "Vehicle.MSRP" }));
            if (Vehicle.MSRP < Vehicle.SalePrice)
                errors.Add(new ValidationResult("Sale Price must be Less than MSRP", new[] { "Vehicle.SalePrice","Vehicle.MSRP" }));
            if (Vehicle.SalePrice < 0)
                errors.Add(new ValidationResult("Sale Price must be positive", new[] { "Vehicle.SalePrice" }));
            if (string.IsNullOrEmpty(Vehicle.Description))
                errors.Add(new ValidationResult("Description is requried", new[] { "Vehicle.Description" }));
            
            return errors;
        }
    }
}