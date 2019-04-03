using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuildCars.Data.EF;

namespace GuildCars.ViewModels
{
    public class HomeIndexVM
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<Special> Specials { get; set; }
    }
}