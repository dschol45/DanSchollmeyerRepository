using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;

namespace GuildCars.Data.Repo
{
    public class SpecialRepo : ISpecialRepo
    {
        public IEnumerable<Special> GetAll()
        {
            throw new NotImplementedException();
        }

        public Special GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Special special)
        {

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
