using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.EF;
using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models;
using GuildCars.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GuildCars.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IVehicleRepo vRepo = Factory.GetVehicleRepo();
        IModelRepo modelRepo = Factory.GetModelRepo();
        IMakeRepo makeRepo = Factory.GetMakeRepo();
        ISpecialRepo specialRepo = Factory.GetSpecialRepo();

        //SecurityRepo secRepo = new SecurityRepo();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vehicles()
        {
            var model = vRepo.GetAll().Where(v => v.Type != "Sold");
            foreach (var vehicle in model)
            {
                vehicle.Make = makeRepo.GetById(vehicle.MakeId);
                vehicle.Model = modelRepo.GetById(vehicle.ModelId);
            }
            return View(model);
        }

        public ActionResult AddVehicle()
        {
            VehicleAdminVM adminVM = new VehicleAdminVM();
            adminVM.SetAllListItems();
            adminVM.Makes = new SelectList(makeRepo.GetAll(), "MakeId", "MakeName");
            adminVM.Models = new SelectList(modelRepo.GetAll(), "ModelId", "ModelName");
            adminVM.Vehicle = new Vehicle();
            adminVM.FileType = ".";

            return View(adminVM);
        }

        [HttpPost]
        public ActionResult AddVehicle(VehicleAdminVM vavm)
        {
            vavm.SetAllListItems();
            vavm.Makes = new SelectList(makeRepo.GetAll(), "MakeId", "MakeName");
            vavm.Models = new SelectList(modelRepo.GetAll(), "ModelId", "ModelName");
            IEnumerable<Make> allMakes = makeRepo.GetAll();
            IEnumerable<Model> allModels = modelRepo.GetAll();
            vavm.Vehicle.Model = modelRepo.GetById(vavm.Vehicle.ModelId);

            if (!allMakes.Any(m => m.MakeId == vavm.Vehicle.MakeId))
            {
                ModelState.AddModelError("Vehicle.MakeId", "Invalid Make selection");
            }
            if (!allModels.Any(m => m.ModelId == vavm.Vehicle.ModelId))
            {
                ModelState.AddModelError("Vehicle.ModelId", "Invalid Model selection");
            }
            if (vavm.Vehicle.Model == null || vavm.Vehicle.MakeId != vavm.Vehicle.Model.MakeId)
            {
                ModelState.AddModelError("Vehicle.ModelId", "Model not available for selected Make");
            }
            if (vavm.UploadedPic == null)
            {
                ModelState.AddModelError("UploadedPic", "Picture is required");
            }

            if (ModelState.IsValid)
            {
                if (vavm.UploadedPic != null && vavm.UploadedPic.ContentLength > 0)
                {
                    string type = "." + vavm.UploadedPic.ContentType.Substring(6);
                    vavm.FileType = type;
                    if (type == ".png" || type == ".jpg" || type == ".jpeg")
                    {
                        //add in other form validation
                        int id = vRepo.Create(vavm.Vehicle);
                        vavm.Vehicle.VehicleId = id;

                        string path = Path.Combine(Server.MapPath("~/Images/"), "inventory-" + id + ".png");
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        vavm.UploadedPic.SaveAs(path);

                        return RedirectToAction("EditVehicle/" + vavm.Vehicle.VehicleId);
                    }
                }
            }
            return View(vavm);
        }

        public ActionResult EditVehicle(int id)
        {

            // validate for if sold

            VehicleAdminVM adminVM = new VehicleAdminVM();
            adminVM.SetAllListItems();
            adminVM.Makes = new SelectList(makeRepo.GetAll(), "MakeId", "MakeName");
            adminVM.Models = new SelectList(modelRepo.GetAll(), "ModelId", "ModelName");
            adminVM.Vehicle = vRepo.GetById(id);
            adminVM.Vehicle.Make = makeRepo.GetById(adminVM.Vehicle.MakeId);
            adminVM.Vehicle.Model = modelRepo.GetById(adminVM.Vehicle.ModelId);
            adminVM.FileType = ".png";
            return View(adminVM);
        }

        [HttpPost]
        public ActionResult EditVehicle(VehicleAdminVM vavm)
        {
            vavm.SetAllListItems();
            vavm.Makes = new SelectList(makeRepo.GetAll(), "MakeId", "MakeName");
            vavm.Models = new SelectList(modelRepo.GetAll(), "ModelId", "ModelName");
            IEnumerable<Make> allMakes = makeRepo.GetAll();
            IEnumerable<Model> allModels = modelRepo.GetAll();
            vavm.Vehicle.Model = modelRepo.GetById(vavm.Vehicle.ModelId);

            if (!allMakes.Any(m => m.MakeId == vavm.Vehicle.MakeId))
            {
                ModelState.AddModelError("Vehicle.MakeId", "Invalid Make selection");
            }
            if (!allModels.Any(m => m.ModelId == vavm.Vehicle.ModelId))
            {
                ModelState.AddModelError("Vehicle.ModelId", "Invalid Model selection");
            }
            if (vavm.Vehicle.Model == null || vavm.Vehicle.MakeId != vavm.Vehicle.Model.MakeId)
            {
                ModelState.AddModelError("Vehicle.ModelId", "Model not available for selected Make");
            }


            if (ModelState.IsValid)
            {
                if (vavm.UploadedPic != null && vavm.UploadedPic.ContentLength > 0)
                {
                    string type = "." + vavm.UploadedPic.ContentType.Substring(6);
                    vavm.FileType = type;
                    if (type == ".png" || type == ".jpg" || type == ".jpeg")
                    {
                        //add in other form validation

                        string path = Path.Combine(Server.MapPath("~/Images/"), "inventory-" + vavm.Vehicle.VehicleId + ".png");
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        vavm.UploadedPic.SaveAs(path);
                    }
                }

                vRepo.Update(vavm.Vehicle);
                return RedirectToAction("Vehicles");
            }
            return View(vavm);
        }


        [HttpPost]
        public ActionResult DeleteVehicle(int id)
        {
            if (vRepo.GetAll().Any(v => v.VehicleId == id))
            {
                vRepo.Delete(id);

                string path = Path.Combine(Server.MapPath("~/Images/"), "inventory-" + id + ".png");
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                return RedirectToAction("Vehicles");

            }
            return RedirectToAction("EditVehicle/"+id);
        }

        public ActionResult Makes()
        {
            MakeVM makeVM = new MakeVM();
            makeVM.Makes = makeRepo.GetAll();

            return View(makeVM);
        }

        [HttpPost]
        public ActionResult Makes(MakeVM makeVM)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            makeVM.Make.UserId = authManager.User.Identity.GetUserId();

            makeRepo.Create(makeVM.Make);
            return RedirectToAction("Makes");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Models()
        {
            ModelVM modelVM = new ModelVM();
            modelVM.Models = modelRepo.GetAll();
            modelVM.Makes = new SelectList(makeRepo.GetAll(), "MakeId", "MakeName");
            return View(modelVM);
        }

        [HttpPost]
        public ActionResult Models(ModelVM modelVM)
        {
            var makes = makeRepo.GetAll();
            Make make = makeRepo.GetById(modelVM.Model.MakeId);
            if (makes.Contains(make))
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                modelVM.Model.UserId = authManager.User.Identity.GetUserId();

                modelRepo.Create(modelVM.Model);
                return RedirectToAction("Models");
            }
            else
            { return View(); }

        }

        public ActionResult Specials()
        {
            SpecialVM specialVM = new SpecialVM();
            specialVM.Specials = specialRepo.GetAll();

            return View(specialVM);
        }

        [HttpPost]
        public ActionResult Specials(SpecialVM specialVM)
        {
            specialRepo.Create(specialVM.Special);
            return RedirectToAction("Specials");
        }

        [HttpPost]
        public ActionResult DeleteSpecial(int id)
        {
            specialRepo.Delete(id);
            return RedirectToAction("Specials");
        }

        [HttpGet]
        public ActionResult Users()
        {
            IUserRepo userRepo = Factory.GetUserRepo();
            IEnumerable<User> users;
            users = userRepo.GetAll();
            return View(users);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            UserVM model = new UserVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
                var user = new AppUser()
                {
                    UserName = userVM.Email,
                    Email = userVM.Email
                };

                var result = userMgr.Create(user, userVM.Password);

                if (result.Succeeded)
                {
                    Claim lastName = new Claim("LastName", userVM.LastName);
                    Claim firstName = new Claim("FirstName", userVM.FirstName);

                    userMgr.AddClaim(user.Id, lastName);
                    userMgr.AddClaim(user.Id, firstName);

                    userMgr.AddToRole(user.Id, userVM.Role);
                    return RedirectToAction("Users");
                }
            }
            return View(userVM);
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            var userRepo = Factory.GetUserRepo();

            User user = userRepo.GetById(id);
            UserVM model = new UserVM();

            model.UserId = user.UserId;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Role = user.Role;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserVM userVM)
        {
            var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var oldUser = userMgr.FindById(userVM.UserId);

            userMgr.Delete(oldUser);

            var user = new AppUser()
            {
                UserName = userVM.Email,
                Email = userVM.Email
            };

            var result = userMgr.Create(user, userVM.Password);
            bool success = result.Succeeded;

            Claim lastName = new Claim("LastName", userVM.LastName);
            Claim firstName = new Claim("FirstName", userVM.FirstName);

            userMgr.AddClaim(user.Id, lastName);
            userMgr.AddClaim(user.Id, firstName);

            userMgr.AddToRole(user.Id, userVM.Role);

            if (success)
            {
                return RedirectToAction("Users");
            }
            else
            {
                return View(userVM);
            }
        }

        [HttpGet]
        public ActionResult ChangePassword(string id)
        {
            var userRepo = Factory.GetUserRepo();
            User user = userRepo.GetById(id);
            UserVM model = new UserVM();

            model.UserId = user.UserId;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Role = user.Role;

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(UserVM userVM)
        {
            var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var oldUser = userMgr.FindById(userVM.UserId);

            userMgr.RemovePassword(userVM.UserId);
            userMgr.AddPassword(userVM.UserId, userVM.Password);

            return RedirectToAction("Users");
        }
    }
}