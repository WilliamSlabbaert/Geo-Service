using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Managers
{
    public class CityManager
    {
        public IUnitOfWork uow;
        public CityManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void AddCity(City con)
        {
            uow.cityRepo.add(con);
            uow.Complete();
        }
    }
}
