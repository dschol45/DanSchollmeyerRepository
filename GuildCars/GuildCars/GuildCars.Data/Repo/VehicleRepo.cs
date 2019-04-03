using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;

namespace GuildCars.Data.Repo
{
    public class VehicleRepo : IVehicleRepo
    {
        public int Create(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> GetAll()
        {
            throw new NotImplementedException();
        }

        public Vehicle GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> Search(string type, string term, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        {
            throw new NotImplementedException();
        }

        public void Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public void UpdateType(int id)
        {
            throw new NotImplementedException();
        }
    }
}
