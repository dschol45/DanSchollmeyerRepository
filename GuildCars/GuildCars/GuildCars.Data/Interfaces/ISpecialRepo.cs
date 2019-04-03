using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;

namespace GuildCars.Data.Interfaces
{
    public interface ISpecialRepo
    {
        IEnumerable<Special> GetAll();
        Special GetById(int id);
        void Create(Special special);
        void Delete(int id);
    }
}
