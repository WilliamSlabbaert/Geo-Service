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
                con.SetContinent(context.ContinentData.FirstOrDefault(s => s.Name == con.Continent.Name));
                context.CountryData.Add(con);
                context.SaveChanges();
                //RefactorPopulation();

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong : CountryRepo add " + e);
            }
        }
        public void delete(int id)
        {
            try
            {
                Continent con = getById(id).Continent;
                context.CountryData.Remove(getById(id));
                context.SaveChanges();
                //RefactorPopulation();
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
                return context.CountryData.Include(s => s.Continent).Include(c => c.Cities).Include(c => c.Rivers).ToList();
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
                Country temp = context.CountryData.Include(s=>s.Continent).Include(c =>c.Cities).Include(c => c.Rivers).FirstOrDefault(s => s.ID == id);
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
            catch (Exception e)
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
                //RefactorPopulation();
            }
            catch(Exception e)
            {
                throw new Exception("Something went wrong : CountryRepo update: " +  e.Message );
            }

        }
        public List<Country> getCountryByContinent(Continent con)
        {
            try
            {
                var temp = getAll().FindAll(s => s.Continent == con);
                return temp;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong : CountryRepo getCountryByContinent " + e);
            }
        }
    }
}
