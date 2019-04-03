using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;

namespace GuildCars.Data.Interfaces
{
    public interface IMakeRepo
    {
        IEnumerable<Make> GetAll();
        Make GetById(int id);
        void Create(Make make);
    }
}
