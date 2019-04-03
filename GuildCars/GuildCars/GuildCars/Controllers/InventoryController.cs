using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Repo.Mock;

namespace GuildCars.Controllers
{
    [AllowAnonymous]
    public class InventoryController : Controller
    {
        public static IVehicleRepo vRepo = Factory.GetVehicleRepo();
        public static IModelRepo modelRepo = Factory.GetModelRepo();
        public static IMakeRepo makeRepo = Factory.GetMakeRepo();

        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            
            var vehicle = vRepo.GetById(id);
            vehicle.Make = makeRepo.GetById(vehicle.MakeId);
            vehicle.Model = modelRepo.GetById(vehicle.ModelId);

            return View(vehicle);
        }

        public ActionResult New()
        {
            var vehicles= vRepo.GetAll().Where(v => v.Type == "New");
            foreach (var vehicle in vehicles)
            {
                vehicle.Make = makeRepo.GetById(vehicle.MakeId);
                vehicle.Model = modelRepo.GetById(vehicle.ModelId);
            }

            return View(vehicles);
        }

        public ActionResult Used()
        {
            var vehicles = vRepo.GetAll().Where(v => v.Type == "Used");
            foreach (var vehicle in vehicles)
            {
                vehicle.Make = makeRepo.GetById(vehicle.MakeId);
                vehicle.Model = modelRepo.GetById(vehicle.ModelId);
            }

            return View(vehicles);
        }
    }
}