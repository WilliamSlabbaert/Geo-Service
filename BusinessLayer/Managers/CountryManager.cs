using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Managers
{
    public class CountryManager
    {
        public IUnitOfWork uow;
        public CountryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void AddCountry(Country con)
        {
            uow.countryRepo.add(con);
            uow.Complete();
        }
    }
}
