//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GuildCars.Data.EF
{
    using System;
    using System.Collections.Generic;
    using GuildCars.Models;
    //using System.Web.Mvc;

    public partial class SaleInfo
    {
        public int SaleId { get; set; }
        public string UserId { get; set; }
        public int VehicleId { get; set; }
        public DateTime SaleDate { get; set; }
        public string SaleName { get; set; }
        public string SalePhone { get; set; }
        public string SaleEmail { get; set; }
        public string SaleStreet1 { get; set; }
        public string SaleStreet2 { get; set; }
        public string SaleCity { get; set; }
        public string SaleState { get; set; }
        public decimal SaleZip { get; set; }
        public decimal SalePrice { get; set; }
        public string SaleType { get; set; }
    
        public virtual Vehicle Vehicle { get; set; }
        //public virtual List<SelectListItem> States { get; set; }

        //public void SetAllStateItems(IEnumerable<State> states)
        //{
        //    foreach (var state in states)
        //    {
        //        States.Add(new SelectListItem()
        //        {
        //            Value = state.StateAbbreviation,
        //            Text = state.StateName
        //        });
        //    }
        //}
    }
}
