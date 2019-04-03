using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;

namespace GuildCars.Data.Repo
{
    public class ModelRepo : IModelRepo
    {
        public void Create(Model model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model> GetAll()
        {
            throw new NotImplementedException();
        }

        public Model GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
