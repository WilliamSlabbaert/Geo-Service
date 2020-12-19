using BusinessLayer;
using Datalaag.DataRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datalaag
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;
        public IContinentRepo continentRepo { get; }

        public IRiverRepo riverRepo { get; }

        public ICityRepo cityRepo { get; }

        public ICountryRepo countryRepo { get; }

        public UnitOfWork(DataContext context)
        {
            this.context = context;
            cityRepo = new CityRepo(context);
            countryRepo = new CountryRepo(context);
            continentRepo = new ContinentRepo(context);
            riverRepo = new RiverRepo(context);
            
        }
        public int Complete()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
