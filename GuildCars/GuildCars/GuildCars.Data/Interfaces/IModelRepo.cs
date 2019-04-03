using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;

namespace GuildCars.Data.Interfaces
{
    public interface IModelRepo
    {
        IEnumerable<Model> GetAll();
        Model GetById(int id);
        void Create(Model model);
    }
}
