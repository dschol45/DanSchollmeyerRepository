using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Exercises.Models.Data;

namespace Exercises.Models.ViewModels
{
    public class StateVM
    {
        public State State{ get; set; }
        public List<SelectListItem> StateItems { get; set; }
        public List<SelectListItem> AllStateItems { get; set; }
        public List<SelectListItem> AvailableStateItems { get; set; }


        public StateVM()
        {
            StateItems = new List<SelectListItem>();
            AllStateItems = new List<SelectListItem>();
            AvailableStateItems = new List<SelectListItem>();
            State = new State();
        }

        public void SetStateItems(IEnumerable<State> states)
        {
            foreach (var state in states)
            {
                StateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }
        public void SetAllStateItems(IEnumerable<State> allStates)
        {
            foreach (var state in allStates)
            {
                AllStateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }

        public void SetAvailableStateItems(IEnumerable<State> availableStates)
        {
            foreach (var state in availableStates)
            {
                AvailableStateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }
    }
}