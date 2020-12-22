using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Managers
{
    public class CountryManager
    {
        public IUnitOfWork uow;
        public CountryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void Add(Country con)
        {
            try
            {
                var temp = GetAll().FirstOrDefault(s => s.Name == con.Name && s.Continent.Name == con.Continent.Name);
                if (temp == null)
                    uow.countryRepo.add(con);
                else
                    throw new Exception("Country already Exist in Continent" + temp.ID);
                RefactorPopulation();
            }
            catch (Exception e)
            {
                throw new Exception("CountryManager Add " + e.Message);
            }
        }
        public void Update(Country con,String name,int pop,double sur,Continent continent)
        {
            try
            {
                var temp = GetAll().FindAll(s => s.Name == name && s.Continent == continent);
                temp.Remove(con);
                if (temp.Count == 0)
                {
                    int population = 0;
                    foreach (var city in con.Cities)
                        population += city.Population;

                    if (population <= pop)
                    {
                        con.SetContinent(uow.continentRepo.getById(continent.ID));
                        con.SetName(name);
                        con.SetPopulation(pop);
                        con.SetSurface(sur);
                        uow.Complete();
                    }
                    else
                        throw new Exception("Population set to small ");
                }
                else
                    throw new Exception("Country already Exist in Continent");
                RefactorPopulation();
            }
            catch (Exception e)
            {
                throw new Exception("CountryManager Update " + e);
            }
        }
        public Country Get(int id)
        {
            try
            {
                var temp = uow.countryRepo.getById(id);
                return temp;
            }
            catch (Exception e)
            {
                throw new Exception("CountryManager Get " + e);
            }
        }
        public List<Country> GetAll()
        {
            try
            {
                return uow.countryRepo.getAll();
            }
            catch (Exception e)
            {
                throw new Exception("CountryManager RemoveAll " + e);
            }
        }
        public void Remove(int id)
        {
            try
            {
                if(Get(id).Cities.Count == 0)
                {
                    uow.countryRepo.delete(id);
                    RefactorPopulation();
                }
                else
                    throw new Exception("Country still contains cities");
            }
            catch (Exception e)
            {
                throw new Exception("CountryManager Remove " + e);
            }
        }
        public void RemoveAll()
        {
            try
            {
                foreach(var country in GetAll())
                    if (country.Cities.Count != 0)
                        throw new Exception("there is still a country with cities");

                uow.countryRepo.removeAll();
                RefactorPopulation();
            }
            catch (Exception e)
            {
                throw new Exception("CountryManager RemoveAll " + e.Message);
            }
        }
        public void RefactorPopulation()
        {
            foreach (var con in uow.continentRepo.getAll())
            {
                int count = 0;
                var temp = uow.countryRepo.getCountryByContinent(con);

                if (temp != null && con != null)
                {
                    foreach (var country in temp)
                        count += country.Population;
                }
                con.SetPopulation(count);
                uow.Complete();
            }
        }
        public List<Country> GetByContinentName(Continent con)
        {
            try
            {
                return GetAll().FindAll(s => s.Continent.Name == con.Name);
            }
            catch (Exception e)
            {
                throw new Exception("CountryManager GetByCountryName " + e);
            }
        }
    }
}
