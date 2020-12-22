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
                con.SetCountry(context.CountryData.FirstOrDefault(s => s.Name == con.Country.Name));
                context.CityData.Add(con);
                context.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception("there went something wrong : CityRepo add " +e );
            }
        }

        public void delete(int id)
        {
            try
            {
                var city = getById(id);
                context.CityData.Remove(city);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("there went something wrong : CityRepo delete " + e);
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
        public List<City> getAllByCountry(Country country)
        {
            try
            {
                return context.CityData.Include(s => s.Country).ToList().FindAll(s=>s.Country == country);
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
                City temp = context.CityData.Include(s=>s.Country).ThenInclude(s=>s.Continent).FirstOrDefault(s=>s.ID == id);
                if (temp == null)
                    throw new Exception("there is no City");
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
            catch(Exception e)
            {
                throw new Exception("there went something wrong : CityRepo removeAll " + e);
            }
        }

        public void update(City con)
        {
            try
            {
                var country = context.CountryData.FirstOrDefault(s => s.Name == con.Country.Name);
                var continent = context.ContinentData.FirstOrDefault(s => s.Name == country.Continent.Name);
                country.SetContinent(continent);
                con.SetCountry(country);

                context.CityData.Update(con);
                context.SaveChanges();
                //RefreshPopulation();
            }
            catch
            {
                throw new Exception("there went something wrong : CityRepo update");
            }
        }
        //public void RefreshPopulation()
        //{
        //    foreach (var country in context.CountryData.ToList())
        //    {
        //        country.GetCitiesPopulationCount();
        //        context.CountryData.Update(country);
        //        context.SaveChanges();
        //    }
        //}
    }
}
