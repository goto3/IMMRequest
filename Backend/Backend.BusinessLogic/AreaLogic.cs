using System;
using Backend.Domain;
using Backend.BusinessLogic.Interface;
using Backend.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Backend.BusinessLogic
{
    public class AreaLogic : ILogic<Area>
    {
        private IRepository<Area> areaRepository;

        public AreaLogic(IRepository<Area> areaRepo)
        {
            this.areaRepository = areaRepo;
        }

        public Area Create(Area area)
        {
            Validate(area);
            var existentArea = GetAll().ToList().Find(a => a.Name == area.Name);
            if (existentArea == null)
            {
                areaRepository.Add(area);
                areaRepository.Save();
                return area;
            }
            return existentArea;
        }

        public Area Get(Guid id)
        {
            return areaRepository.Get(id);
        }

        public IEnumerable<Area> GetAll()
        {
            return areaRepository.GetAll();
        }

        public void Remove(Guid id)
        {
            Area area = areaRepository.Get(id);
            areaRepository.Remove(area);
            areaRepository.Save();
        }

        public void Validate(Area entity)
        {
            /* NOMBRE VACIO O NULO*/
        }
    }
}
