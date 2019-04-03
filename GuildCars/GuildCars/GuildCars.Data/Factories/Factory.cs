using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Repo;
using GuildCars.Data.Repo.Mock;

namespace GuildCars.Data.Factories
{
    public class Factory
    {
        public static IVehicleRepo GetVehicleRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Mock":
                    return new VehicleMockRepo();
                case "EF":
                    return new VehicleRepo();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value");
            }
        }

        public static IModelRepo GetModelRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Mock":
                    return new ModelMockRepo();
                case "EF":
                    return new ModelRepo();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value");
            }
        }

        public static IMakeRepo GetMakeRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Mock":
                    return new MakeMockRepo();
                case "EF":
                    return new MakeRepo();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value");
            }
        }

        public static IContactRepo GetContactRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Mock":
                    return new ContactMockRepo();
                case "EF":
                    return new ContactRepo();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value");
            }
        }

        public static ISpecialRepo GetSpecialRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Mock":
                    return new SpecialMockRepo();
                case "EF":
                    return new SpecialRepo();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value");
            }
        }

        public static ISaleInfoRepo GetSaleInfoRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Mock":
                    return new SaleInfoMockRepo();
                case "EF":
                    return new SaleInfoRepo();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value");
            }
        }

        public static IStateRepo GetStateRepo()
        {
            return new StateRepo();
        }

        public static IUserRepo GetUserRepo()
        {
            return new UserRepo();
        }
    }
}
