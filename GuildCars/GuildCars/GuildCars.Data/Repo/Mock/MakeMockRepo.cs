using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;

namespace GuildCars.Data.Repo.Mock
{
    public class MakeMockRepo : IMakeRepo
    {
        private static List<Make> _makes;

        static MakeMockRepo()
        {
            _makes = new List<Make>
           {
               new Make{
                   MakeId =1,UserId="00000000-0000-0000-0000-000000000000",MakeName="Ford",MakeDate=DateTime.ParseExact("2010/01/01","yyyy/dd/MM",CultureInfo.InvariantCulture)},
               new Make{
                   MakeId =2,UserId="11111111-1111-1111-1111-111111111111",MakeName="Chevy",MakeDate=DateTime.ParseExact("2010/01/01","yyyy/dd/MM",CultureInfo.InvariantCulture)},
           };
        }

        public void Create(Make make)
        {
            if (_makes.Any())
            { make.MakeId = _makes.Max(m => m.MakeId) + 1; }
            else
            { make.MakeId = 0; }
            make.MakeDate = DateTime.Now;
            _makes.Add(make);
        }

        public IEnumerable<Make> GetAll()
        {
            IEnumerable<Make> makes = _makes;
            return makes;
        }

        public Make GetById(int id)
        {
            Make make = _makes.FirstOrDefault(v=>v.MakeId == id);
            return make;
        }
    }
}
