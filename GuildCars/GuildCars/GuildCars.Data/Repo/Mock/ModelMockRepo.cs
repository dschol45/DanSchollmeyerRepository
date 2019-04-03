using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;

namespace GuildCars.Data.Repo.Mock
{
    public class ModelMockRepo : IModelRepo
    {
        private static List<Model> _models;

        static ModelMockRepo()
        {
            _models = new List<Model>
           {
               new Model{
                   ModelId =1,MakeId=1,UserId="00000000-0000-0000-0000-000000000000",ModelDate=new DateTime(2010/01/01),ModelName="F-150"},
               new Model{
                   ModelId =2,MakeId=2,UserId="11111111-1111-1111-1111-111111111111",ModelName="Cobalt",ModelDate=new DateTime(2020/02/02)},
           };
        }

        public void Create(Model model)
        {
            if (_models.Any())
            { model.MakeId = _models.Max(m => m.MakeId) + 1; }
            else
            { model.MakeId = 0; }
            model.ModelDate = DateTime.Now;
            _models.Add(model);
        }

        public IEnumerable<Model> GetAll()
        {
            IEnumerable<Model> models = _models;
            return models;
        }

        public Model GetById(int id)
        {
            Model model = _models.FirstOrDefault(v => v.ModelId == id);
            return model;
        }
    }
}
