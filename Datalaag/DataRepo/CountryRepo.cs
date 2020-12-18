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
                if (NameCheck(con))
                {
                    context.CountryData.Add(con);
                    context.SaveChanges();
                    RefactorPopulation();
                }
                else
                    throw new Exception("Country already exist instide Continent");
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
                RefactorPopulation();
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
                Country temp = context.CountryData.Include(s => s.Continent).FirstOrDefault(s => s.ID == id);
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
                RefactorPopulation();
            }
            catch
            {
                throw new Exception("Something went wrong : CountryRepo update");
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
        public Boolean NameCheck(Country con)
        {
            try
            {
                List<Country> CountriesTemp = getCountryByContinent(con.Continent);
                Country temp = CountriesTemp.FirstOrDefault(s => s.Name == con.Name);
                if (temp == null)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong : CountryRepo NameCheck " + e);
            }
        }
        public void RefactorPopulation()
        {
            foreach (var con in context.ContinentData.ToList())
            {
                int count = 0;
                var temp = getCountryByContinent(con);

                if (temp != null && con != null)
                {
                    foreach (var country in temp)
                        count += country.Population;
                }
                con.SetPopulation(count);
                context.SaveChanges();
            }
        }
    }
}
