namespace GuildCars.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GuildCars.Data.EF;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCarsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCarsDbContext context)
        {
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(context));

            if (!roleMgr.RoleExists("admin"))
            {
                roleMgr.Create(new AppRole() { Name = "admin" });

                var user = new AppUser()
                {
                    UserName = "admin"
                };

                userMgr.Create(user, "testing123");

                userMgr.AddToRole(user.Id, "admin");
            }

            if (!roleMgr.RoleExists("sales"))
            {
                roleMgr.Create(new AppRole() { Name = "sales" });
            }

            if (!roleMgr.RoleExists("disabled"))
            {
                roleMgr.Create(new AppRole() { Name = "disabled" });
            }
        }
    }
}
