using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;

namespace GuildCars.Data.Repo.Mock
{
    public class VehicleMockRepo : IVehicleRepo
    {
        private static List<Vehicle> _vehicles;

        static VehicleMockRepo()
        {
            _vehicles = new List<Vehicle>
           {
               new Vehicle{
                   VehicleId =0,  ModelId=1, MakeId=1, Year=2000, Description="In good condition", Type="Used", IsFeatured=true, BodyStyle="Truck", Transmission="Automatic", Color="Red", Interior="Green", Mileage=123456, VIN="1AAAA11111A111111", SalePrice=1000.00m, MSRP=1001.00m},
               new Vehicle{
                   VehicleId =1,  ModelId=2, MakeId=2, Year=2019, Description="In great condition", Type="New", IsFeatured=true, BodyStyle="Car", Transmission="Manual", Color="Black", Interior="Yellow", Mileage=1, VIN="2BBBB22222B222222", SalePrice=50000.00m, MSRP=52000.00m},
               new Vehicle{
                   VehicleId =2,  ModelId=1, MakeId=1, Year=2020, Description="In great condition", Type="Sold", IsFeatured=false, BodyStyle="Truck", Transmission="Manual", Color="Orange", Interior="Green", Mileage=2, VIN="3CCCC33333C333333", SalePrice=5000.00m, MSRP=5100.01m},
               new Vehicle{
                   VehicleId =3,  ModelId=2, MakeId=2, Year=2003, Description="Barney", Type="Used", IsFeatured=false, BodyStyle="Car", Transmission="Automatic", Color="Purple", Interior="Pink", Mileage=54321, VIN="4DDDD44444D444444", SalePrice=5500.00m, MSRP=5500.00m},
               new Vehicle{
                   VehicleId =4,  ModelId=1, MakeId=1, Year=2000, Description="In good condition", Type="Used", IsFeatured=true, BodyStyle="Truck", Transmission="Automatic", Color="Red", Interior="Green", Mileage=87459, VIN="5EEEE55555E555555", SalePrice=5319.00m, MSRP=5320.00m},
               new Vehicle{
                   VehicleId =5,  ModelId=2, MakeId=2, Year=2019, Description="In great condition", Type="New", IsFeatured=true, BodyStyle="Car", Transmission="Manual", Color="Black", Interior="Yellow", Mileage=1, VIN="6FFFF66666F666666", SalePrice=8700.00m, MSRP=8800.00m}
           };
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return _vehicles;
        }

        public Vehicle GetById(int id)
        {
            Vehicle vehicle = _vehicles.FirstOrDefault(v=>v.VehicleId == id);
            return vehicle;
        }

        public IEnumerable<Vehicle> Search(string type, string term, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        {
            IModelRepo modelRepo = Factory.GetModelRepo();
            IMakeRepo makeRepo = Factory.GetMakeRepo();
            IEnumerable<Vehicle> vehicles = new List<Vehicle>();
            switch (type)
            {
                case "new":
                    vehicles = GetAll().Where(v => v.Type == "New");
                    break;
                case "used":
                    vehicles = GetAll().Where(v => v.Type == "Used");
                    break;
                case "all":
                    vehicles = GetAll().Where(v => v.Type != "Sold");
                    break;
                default:
                    break;
            }
            List<Vehicle> found = new List<Vehicle>();

            //validated here so we don't pass in another parameter
            int year = 0;
            int.TryParse(term, out year);

            foreach (var vehicle in vehicles)
            {
                vehicle.Make = makeRepo.GetById(vehicle.MakeId);
                vehicle.Model = modelRepo.GetById(vehicle.ModelId);

                if (vehicle.Year >= minYear && vehicle.Year <= maxYear && vehicle.SalePrice >= minPrice && vehicle.SalePrice <= maxPrice)
                {
                    if (term != "hamster")
                    {
                        if (vehicle.Year == year || vehicle.Make.MakeName.ToLower().Contains(term.ToLower()) || vehicle.Model.ModelName.ToLower().Contains(term.ToLower()))
                        {
                            found.Add(vehicle);
                        }
                    }
                    else
                    {
                        found.Add(vehicle);
                    }
                }
            }

            vehicles = found;
            return vehicles;
        }

        public void UpdateType(int id)
        {
            var vehicle = GetById(id);
            _vehicles.Remove(vehicle);
            vehicle.IsFeatured = false;
            vehicle.Type = "Sold";
            _vehicles.Add(vehicle);
        }

        public int Create(Vehicle vehicle)
        {
            if (_vehicles.Any())
            { vehicle.VehicleId = _vehicles.Max(v => v.VehicleId) + 1; }
            else
            { vehicle.VehicleId = 0; }

            _vehicles.Add(vehicle);
            return vehicle.VehicleId;
        }

        public void Update(Vehicle vehicle)
        {
            _vehicles.Remove(_vehicles.FirstOrDefault(v => v.VehicleId == vehicle.VehicleId));
            _vehicles.Add(vehicle);
        }

        public void Delete(int id)
        {
            _vehicles.Remove(GetById(id));
        }
        //public IEnumerable<Vehicle> SearchUsed(string term, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        //{
        //    IModelRepo modelRepo = Factory.GetModelRepo();
        //    IMakeRepo makeRepo = Factory.GetMakeRepo();
        //    IEnumerable<Vehicle> vehicles = GetAll().Where(v => v.Type == "Used");
        //    List<Vehicle> found = new List<Vehicle>();
        //    int year = 0;
        //    int.TryParse(term, out year);

        //    foreach (var vehicle in vehicles)
        //    {
        //        vehicle.Make = makeRepo.GetById(vehicle.MakeId);
        //        vehicle.Model = modelRepo.GetById(vehicle.ModelId);

        //        if (vehicle.Year >= minYear && vehicle.Year <= maxYear && vehicle.SalePrice >= minPrice && vehicle.SalePrice <= maxPrice)
        //        {
        //            if (term != "hamster")
        //            {
        //                if (vehicle.Year == year || vehicle.Make.MakeName.ToLower().Contains(term.ToLower()) || vehicle.Model.ModelName.ToLower().Contains(term.ToLower()))
        //                {
        //                    found.Add(vehicle);
        //                }
        //            }
        //            else
        //            {
        //                found.Add(vehicle);
        //            }
        //        }
        //    }

        //    vehicles = found;
        //    return vehicles;
        //}

        //public IEnumerable<Vehicle> SearchAll(string term, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        //{
        //    IModelRepo modelRepo = Factory.GetModelRepo();
        //    IMakeRepo makeRepo = Factory.GetMakeRepo();
        //    IEnumerable<Vehicle> vehicles = GetAll();
        //    List<Vehicle> found = new List<Vehicle>();
        //    int year = 0;
        //    int.TryParse(term, out year);

        //    foreach (var vehicle in vehicles)
        //    {
        //        vehicle.Make = makeRepo.GetById(vehicle.MakeId);
        //        vehicle.Model = modelRepo.GetById(vehicle.ModelId);

        //        if (vehicle.Year >= minYear && vehicle.Year <= maxYear && vehicle.SalePrice >= minPrice && vehicle.SalePrice <= maxPrice)
        //        {
        //            if (term != "hamster")
        //            {
        //                if (vehicle.Year == year || vehicle.Make.MakeName.ToLower().Contains(term.ToLower()) || vehicle.Model.ModelName.ToLower().Contains(term.ToLower()))
        //                {
        //                    found.Add(vehicle);
        //                }
        //            }
        //            else
        //            {
        //                found.Add(vehicle);
        //            }
        //        }
        //    }

        //    vehicles = found;
        //    return vehicles;
        //}


    }
}
