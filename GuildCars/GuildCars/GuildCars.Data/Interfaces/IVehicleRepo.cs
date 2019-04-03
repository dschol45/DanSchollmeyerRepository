using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;

namespace GuildCars.Data.Interfaces
{
    public interface IVehicleRepo
    {
        IEnumerable<Vehicle> GetAll();
        Vehicle GetById(int id);
        IEnumerable<Vehicle> Search(string type,string term, decimal minPrice, decimal maxPrice, int minYear, int maxYear);
        //IEnumerable<Vehicle> SearchUsed(string term, decimal minPrice, decimal maxPrice, int minYear, int maxYear);
        //IEnumerable<Vehicle> SearchAll(string term, decimal minPrice, decimal maxPrice, int minYear, int maxYear);
        void UpdateType(int id);
        int Create(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(int id);
    }
}
