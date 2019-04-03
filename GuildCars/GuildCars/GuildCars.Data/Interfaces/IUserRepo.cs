using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models;

namespace GuildCars.Data.Interfaces
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAll();
        User GetById(string id);
    }
}
