using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalaag.DataRepo
{
    class CountryRepo : ICountryRepo
    {
        private DataContext context;

        public CountryRepo(DataContext context)
        {
            this.context = context;
        }

        public void add(Country con)
        {
            context.CountryData.Add(con);
        }

        public void delete(int id)
        {
            context.CountryData.Remove(getById(id));
        }

        public List<Country> getAll()
        {
            return context.CountryData.Include(s => s.Cities).Include(s =>s.Rivers).ToList();
        }

        public Country getById(int id)
        {
            Country temp = context.CountryData.Include(s => s.Cities).Include(s => s.Rivers).ToList()[0];
            if (temp == null)
                return null;
            else
                return temp;
        }

        public void removeAll()
        {
            foreach (Country item in getAll())
                delete(item.ID);
        }

        public void update(Country con)
        {
            context.CountryData.Update(con);
        }
    }
}
