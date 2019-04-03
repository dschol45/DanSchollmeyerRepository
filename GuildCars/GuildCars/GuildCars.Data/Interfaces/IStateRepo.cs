using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models;

namespace GuildCars.Data.Interfaces
{
    public interface IStateRepo
    {
        IEnumerable<State> AllStates();
        State GetState(string stateAbbreviation);
    }
}
