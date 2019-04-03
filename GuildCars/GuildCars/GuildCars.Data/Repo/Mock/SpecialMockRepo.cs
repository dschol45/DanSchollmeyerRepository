using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;


namespace GuildCars.Data.Repo.Mock
{
    public class SpecialMockRepo : ISpecialRepo
    {
        private static List<Special> _specials;

        static SpecialMockRepo()
        {
            _specials = new List<Special>
           {
               new Special{
                   SpecialId =0,SpecialTitle="Good",SpecialNote="A good special"},
               new Special{
                   SpecialId =1,SpecialTitle="Great",SpecialNote="A great special"},
           };
        }

        public IEnumerable<Special> GetAll()
        {
            IEnumerable<Special> specials = _specials;
            return specials;
        }

        public Special GetById(int id)
        {
            Special special = _specials.FirstOrDefault(v => v.SpecialId == id);
            return special;
        }

        public void Create(Special special)
        {
            if (_specials.Any())
            { special.SpecialId = _specials.Max(m => m.SpecialId) + 1; }
            else
            { special.SpecialId = 0; }

            _specials.Add(special);
        }

        public void Delete(int id)
        {
            _specials.Remove(GetById(id));
        }
    }
}
