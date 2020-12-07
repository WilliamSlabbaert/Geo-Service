using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalaag.DataRepo
{
    class CityRepo
    {

        private DataContext context;
        public void add(City con)
        {
            context.CityData.Add(con);
        }

        public void delete(int id)
        {
            context.CityData.Remove(getById(id));
        }

        public List<City> getAll()
        {
            return context.CityData.Include(s => s.Country).ToList();
        }

        public City getById(int id)
        {
            City temp = context.CityData.Include(s => s.Country).Where(s => s.ID == id).ToList()[0];
            if (temp == null)
                return null;
            else
                return temp;
        }

        public void removeAll()
        {
            foreach (City item in getAll())
                delete(item.ID);
        }

        public void update(City con)
        {
            context.CityData.Update(con);
        }
    }
}
