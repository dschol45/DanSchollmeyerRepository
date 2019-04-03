using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GuildCars.Data.EF
{
    public class GuildCarsDbContext : IdentityDbContext<AppUser>
    {
        public GuildCarsDbContext() : base("DefaultConnection")
        {

        }

        public static GuildCarsDbContext Create()
        {
            return new GuildCarsDbContext();
        }
    }
}
