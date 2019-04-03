using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.EF;

namespace GuildCars.ViewModels
{
    public class ModelVM
    {
        public IEnumerable<Model> Models { get; set; }
        public IEnumerable<SelectListItem> Makes { get; set; }
        public Model Model { get; set; }

        public void SetMakesList(IEnumerable<Make> makeList)
        {
            List<SelectListItem> makes = new List<SelectListItem>();
            foreach (var item in makeList)
            {
                makes.Add(new SelectListItem()
                {
                    Value = item.MakeId.ToString(),
                    Text = item.MakeName
                });
            }
        }
    }
}