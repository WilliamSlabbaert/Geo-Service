using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IUnitOfWork
    {
        public int Complete();
        public void Dispose();
        public IContinentRepo continentRepo { get; }
        public IRiverRepo riverRepo { get; }
        public ICityRepo cityRepo { get; }
        public ICountryRepo countryRepo { get; }
    }
}
