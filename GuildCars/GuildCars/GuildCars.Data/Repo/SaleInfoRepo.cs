using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;

namespace GuildCars.Data.Repo
{
    public class SaleInfoRepo : ISaleInfoRepo
    {
        public void Create(SaleInfo saleInfo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SaleInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public SaleInfo GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SaleInfo> GetBySearch(string user, string minDate, string maxDate)
        {
            throw new NotImplementedException();
        }
    }
}
