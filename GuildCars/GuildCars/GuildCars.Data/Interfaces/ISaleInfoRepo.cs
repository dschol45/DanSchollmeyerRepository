using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;

namespace GuildCars.Data.Interfaces
{
    public interface ISaleInfoRepo
    {
        IEnumerable<SaleInfo> GetAll();
        SaleInfo GetById(int id);
        void Create(SaleInfo saleInfo);
        IEnumerable<SaleInfo> GetBySearch(string user, string minDate, string maxDate);
    }
}
