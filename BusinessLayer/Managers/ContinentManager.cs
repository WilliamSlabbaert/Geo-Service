using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ContinentManager
    {
        public IUnitOfWork uow;
        public ContinentManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void AddContinent(Continent con)
        {
            uow.continentRepo.add(con);
            uow.Complete();
        }
    }
}
