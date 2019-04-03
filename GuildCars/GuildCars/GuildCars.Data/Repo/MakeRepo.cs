using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;


namespace GuildCars.Data.Repo
{
    public class MakeRepo : IMakeRepo
    {
        public void Create(Make make)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Make> GetAll()
        {
            throw new NotImplementedException();
        }

        public Make GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
