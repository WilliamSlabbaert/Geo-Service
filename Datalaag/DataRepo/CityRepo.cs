using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalaag.DataRepo
{
    public class CityRepo : ICityRepo
    {
        private DataContext context;

        public CityRepo(DataContext context)
        {
            try
            {
                this.context = context;
            }
            catch
            {
                throw new Exception("there went something wrong : CityRepo");
            }
        }

        public void add(City con)
        {
            try
            {
                context.CityData.Add(con);
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("there went something wrong : CityRepo add");
            }
        }

        public void delete(int id)
        {
            try
            {
                context.CityData.Remove(getById(id));
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("there went something wrong : CityRepo delete");
            }
        }

        public List<City> getAll()
        {
            try
            {
                return context.CityData.Include(s => s.Country).ToList();
            }
            catch
            {
                throw new Exception("there went something wrong : CityRepo getAll");
            }
        }

        public City getById(int id)
        {
            try
            {
                City temp = context.CityData.Include(s => s.Country).Where(s => s.ID == id).ToList()[0];
                if (temp == null)
                    return null;
                else
                    return temp;
            }
            catch
            {
                throw new Exception("there went something wrong : CityRepo getById");
            }
        }

        public void removeAll()
        {
            try
            {
                foreach (City item in getAll())
                    delete(item.ID);
            }
            catch
            {
                throw new Exception("there went something wrong : CityRepo removeAll");
            }
        }

        public void update(City con)
        {
            try
            {
                context.CityData.Update(con);
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("there went something wrong : CityRepo update");
            }
        }
    }
}
