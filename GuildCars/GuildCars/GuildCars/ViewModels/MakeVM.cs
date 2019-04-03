using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuildCars.Data.EF;

namespace GuildCars.ViewModels
{
    public class MakeVM
    {
        public IEnumerable<Make> Makes { get; set; }
        public Make Make { get; set; }
    }
}