using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datalaag
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public IContinentRepo continentRepo { get;}

        public IRiverRepo riverRepo { get; }

        public ICityRepo cityRepo { get; }

        public ICountryRepo countryRepo { get; }

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
