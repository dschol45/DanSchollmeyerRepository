using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuildCars.Data.EF;

namespace GuildCars.ViewModels
{
    public class SpecialVM
    {
        public IEnumerable<Special> Specials { get; set; }
        public Special Special { get; set; }
    }
}