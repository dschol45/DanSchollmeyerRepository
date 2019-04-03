using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.EF;
using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Repo;
using GuildCars.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GuildCars.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        public IVehicleRepo vRepo = Factory.GetVehicleRepo();
        public IModelRepo modelRepo = Factory.GetModelRepo();
        public IMakeRepo makeRepo = Factory.GetMakeRepo();
        public ISaleInfoRepo saleRepo = Factory.GetSaleInfoRepo();
        
        public ActionResult Index()
        {
            var model = vRepo.GetAll().Where(v => v.Type != "Sold");
            foreach (var vehicle in model)
            {
                vehicle.Make = makeRepo.GetById(vehicle.MakeId);
                vehicle.Model = modelRepo.GetById(vehicle.ModelId);
            }
            return View(model);
        }


        public ActionResult Purchase(int id)
        {
            var vehicle = vRepo.GetById(id);
                vehicle.Make = makeRepo.GetById(vehicle.MakeId);
                vehicle.Model = modelRepo.GetById(vehicle.ModelId);
            SaleInfoViewModel sale = new SaleInfoViewModel();
            sale.Vehicle = vehicle;

            IStateRepo stateRepo = Factory.GetStateRepo();
            sale.SetAllStateItems(stateRepo.AllStates());
            if (sale.Vehicle.Type == "Sold")
            {
                return RedirectToAction("Index");
            }
            return View(sale);
        }

        [HttpPost]
        public ActionResult Purchase(SaleInfoViewModel sivm)
        {
            sivm.Vehicle = vRepo.GetById(sivm.Vehicle.VehicleId);

            if (ModelState.IsValid)
            {
                vRepo.UpdateType(sivm.Vehicle.VehicleId);
                
                var authManager = HttpContext.GetOwinContext().Authentication;
                sivm.SaleInfo.UserId = authManager.User.Identity.GetUserId();
                sivm.SaleInfo.VehicleId = sivm.Vehicle.VehicleId;

                saleRepo.Create(sivm.SaleInfo);
                return RedirectToAction("Index");
            }

            sivm.Vehicle = vRepo.GetById(sivm.Vehicle.VehicleId);
            sivm.Vehicle.Make = makeRepo.GetById(sivm.Vehicle.MakeId);
            sivm.Vehicle.Model = modelRepo.GetById(sivm.Vehicle.ModelId);

            IStateRepo stateRepo = Factory.GetStateRepo();
            sivm.SetAllStateItems(stateRepo.AllStates());
            return View(sivm);            
        }
    }
}