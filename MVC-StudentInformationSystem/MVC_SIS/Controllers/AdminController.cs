using Exercises.Models.Data;
using Exercises.Models.Repositories;
using Exercises.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercises.Controllers
{
    public class AdminController : Controller
    {
        //Majors Methods
        [HttpGet]
        public ActionResult Majors()
        {
            var model = MajorRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddMajor()
        {
            return View(new Major());
        }

        [HttpPost]
        public ActionResult AddMajor(Major major)
        {
            if (string.IsNullOrEmpty(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please enter a Major Name");
                return View("AddMajor", major);
            }
            else
            {
                MajorRepository.Add(major.MajorName);
                return RedirectToAction("Majors");
            }
        }

        [HttpGet]
        public ActionResult EditMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult EditMajor(Major major)
        {
            if (string.IsNullOrEmpty(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please enter a Major Name");
                return View("EditMajor", major);
            }
            else
            {
                MajorRepository.Edit(major);
                return RedirectToAction("Majors");
            }
        }

        [HttpGet]
        public ActionResult DeleteMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult DeleteMajor(Major major)
        {
            MajorRepository.Delete(major.MajorId);
            return RedirectToAction("Majors");
        }

        //states methods
        [HttpGet]
        public ActionResult States()
        {
            var model = StateRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddState2()
        {
            StateVM svm = new StateVM();
            svm.SetStateItems(StateRepository.GetAll());
            svm.SetAllStateItems(StateRepository.AllStates());
            svm.SetAvailableStateItems(StateRepository.AvailableStates());

            return View(svm);
        }

        [HttpPost]
        public ActionResult AddState2(StateVM sVM)
        {
            IEnumerable<State> states = StateRepository.GetAll();
            IEnumerable<State> allStates = StateRepository.AllStates();

            if (states.Any(s => s.StateAbbreviation == sVM.State.StateAbbreviation))
            {
                ModelState.AddModelError("StateAbbreviation", "This State Abbreviation is already in use.");
            }
            if (!allStates.Any(s => s.StateAbbreviation == sVM.State.StateAbbreviation))
            {
                ModelState.AddModelError("StateAbbreviation", "Please choose a valid state to add.");
            }
            
            foreach (var item in allStates)
            {
                if (item.StateAbbreviation == sVM.State.StateAbbreviation)
                {
                    sVM.State = item;
                }
            }

            if (ModelState.IsValid)
            {
                
                StateRepository.Add(sVM.State);
                return RedirectToAction("States");
            }
            return View("AddState2", sVM);
        }

        //[HttpGet]
        //public ActionResult AddState2()
        //{
        //    StateVM svm = new StateVM();

        //    svm.SetStateItems(StateRepository.GetAll());
        //    svm.SetAllStateItems(StateRepository.AllStates());

        //    return View(svm);
        //}

        //[HttpPost]
        //public ActionResult AddState2(StateVM sVM)
        //{
        //    IEnumerable<State> states = StateRepository.GetAll();
        //    IEnumerable<State> allStates = StateRepository.AllStates();
        //    if (states.Any(s => s.StateAbbreviation == sVM.State.StateAbbreviation))
        //    {
        //        ModelState.AddModelError("StateAbbreviation", "This State Abbreviation is already in use.");
        //    }
        //    if (!allStates.Any(s => s.StateAbbreviation == sVM.State.StateAbbreviation))
        //    {
        //        ModelState.AddModelError("StateAbbreviation", "Please choose a valid state to add.");
        //    }

        //    foreach (var item in allStates)
        //    {
        //        if (item.StateAbbreviation == sVM.State.StateAbbreviation)
        //        {
        //            sVM.State = item;
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {

        //        StateRepository.Add(sVM.State);
        //        return RedirectToAction("States");
        //    }
        //    return View("AddState2", sVM);
        //}

        [HttpGet]
        public ActionResult EditState(string abbr)
        {
            var state = StateRepository.Get(abbr);
            return View(state);
        }

        //[HttpPost]
        //public ActionResult EditState(State state)
        //{
        //    if (string.IsNullOrEmpty(state.StateName))
        //    {
        //        ModelState.AddModelError("StateName", "Please enter a State Name.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        StateRepository.Edit(state);
        //        return RedirectToAction("States");
        //    }
        //    else
        //    {
        //        return View("EditState", state);
        //    }
        //}

        [HttpGet]
        public ActionResult DeleteState(string abbr)
        {
            var state = StateRepository.Get(abbr);
            return View(state);
        }

        [HttpPost]
        public ActionResult DeleteState(State state)
        {
            StateRepository.Delete(state.StateAbbreviation);
            return RedirectToAction("States");
        }

        //Course Methods
        [HttpGet]
        public ActionResult Courses()
        {
            var model = CourseRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View(new Course());
        }

        [HttpPost]
        public ActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                CourseRepository.Add(course.CourseName);
                return RedirectToAction("Courses");
            }
            else
            {
                return View("AddCourse", course);
            }
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult EditCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                CourseRepository.Edit(course);
                return RedirectToAction("Courses");
            }
            else
            {
                return View("EditCourse", course);
            }
        }

        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult DeleteCourse(Course course)
        {
            CourseRepository.Delete(course.CourseId);
            return RedirectToAction("Courses");
        }
    }
}