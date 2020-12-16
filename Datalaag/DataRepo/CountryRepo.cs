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
            try
            {
                this.context = context;
            }
            catch
            {
                throw new Exception("Something went wrong : CountryRepo");
            }
        }

        public void add(Country con)
        {
            try
            {
                var temp = context.ContinentData.Find(con.Continent.ID);
                temp.AddCountry(con);
                context.ContinentData.Update(temp);
            }
            catch(Exception e)
            {
                throw new Exception("Something went wrong : CountryRepo add " + e);
            }
        }

        public void delete(int id)
        {
            try
            {
                //var con = getById(id).Continent;
                //con.Countries.Remove(getById(id));
                

                context.CountryData.Remove(getById(id));
                context.SaveChanges();
                //context.ContinentData.Update(con);
                //context.SaveChanges();


            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong : CountryRepo delete " + e);
            }
        }

        public List<Country> getAll()
        {
            try
            {
                return context.CountryData.Include(s => s.Cities).Include(s => s.Rivers).ToList();
            }
            catch
            {
                throw new Exception("Something went wrong : CountryRepo delete");
            }
        }

        public Country getById(int id)
        {
            try
            {
                Country temp = context.CountryData.FirstOrDefault(s => s.ID == id);
                if (temp == null)
                    throw new Exception("No Country found");
                else
                    return temp;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong : CountryRepo getById " + e);
            }
        }

        public void removeAll()
        {
            try
            {
                foreach (var item in getAll())
                    delete(item.ID);
            }
            catch(Exception e)
            {
                throw new Exception("Something went wrong : CountryRepo removeAll " + e);
            }
        }

        public void update(Country con)
        {
            try
            {
                context.CountryData.Update(con);
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Something went wrong : CountryRepo update");
            }

        }
    }
}
