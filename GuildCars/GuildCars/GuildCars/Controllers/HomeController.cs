using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Repo.Mock;
using GuildCars.Data.Factories;
using GuildCars.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using GuildCars.Models;

namespace GuildCars.Controllers
{
    public class HomeController : Controller
    {
        public IVehicleRepo vRepo = Factory.GetVehicleRepo();
        public IModelRepo modelRepo = Factory.GetModelRepo();
        public IMakeRepo makeRepo = Factory.GetMakeRepo();
        public ISpecialRepo specialRepo = Factory.GetSpecialRepo();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var specials = specialRepo.GetAll();
            
            var vehicles = vRepo.GetAll().Where(v=> v.IsFeatured == true).Where(w=>w.Type != "Sold");
            foreach (var vehicle in vehicles)
            {
                vehicle.Make = makeRepo.GetById(vehicle.MakeId);
                vehicle.Model = modelRepo.GetById(vehicle.ModelId);
            }

            var model = new HomeIndexVM()
            {
                Vehicles = vehicles,
                Specials = specials
            };

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Contact(string vin)
        {
            Contact contact = new Contact();
            contact.ContactMessage = vin;
            return View(contact);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                IContactRepo repo = Factory.GetContactRepo();
                repo.Insert(contact);

                return RedirectToAction("Index");
            }

            return View(contact);
        }

        [AllowAnonymous]
        public ActionResult Specials()
        {
            IEnumerable<Special> specials = specialRepo.GetAll();
            return View(specials);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginVM();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.GetOwinContext().Authentication;
            AppUser user = userManager.Find(model.UserName, model.Password);

            if (user.Roles.Any(u => u.RoleId == "fab31bab-787c-44ed-8dda-8808b08e4c84"))
            {
                ModelState.AddModelError("", "Your account has been disabled.");
                return View(model);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
            
            else
            {
                var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            var auth = HttpContext.GetOwinContext().Authentication;
            auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            string id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var userRepo = Factory.GetUserRepo();

            User user = userRepo.GetById(id);

            UserVM vm = new UserVM()
            {
                UserId = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(UserVM userVM)
        {
            var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();

            var oldUser = userMgr.FindById(userVM.UserId);

            if (ModelState.IsValid)
            {
                userMgr.RemovePassword(userVM.UserId);

                userMgr.AddPassword(userVM.UserId, userVM.Password);

                return RedirectToAction("Index", "Home");
            }

            return View(userVM);
        }
    }
}