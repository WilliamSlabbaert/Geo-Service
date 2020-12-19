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
                var temp = GetAll().FirstOrDefault(s => s.Name == con.Name && s.Continent == con.Continent);

                if (temp == null)
                    uow.countryRepo.update(con);
                else
                    throw new Exception("Country already Exist in Continent" + temp.ID);
                RefactorPopulation();
            }
            catch (Exception e)
            {
                throw new Exception("CountryManager Add " + e);
            }
        }
        public void Update(Country con,String name,int pop,double sur,Continent continent)
        {
            try
            {
                var temp = GetAll().FirstOrDefault(s => s.Name == name && s.Continent == continent);

                if (temp == null)
                {
                    con.SetContinent(uow.continentRepo.getById(continent.ID));
                    con.SetName(name);
                    con.SetPopulation(pop);
                    con.SetSurface(sur);
                }
                else
                    throw new Exception("Country already Exist in Continent" + temp.ID);
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
                return uow.countryRepo.getById(id);
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
                uow.countryRepo.delete(id);
                RefactorPopulation();
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
                uow.countryRepo.removeAll();
                RefactorPopulation();
            }
            catch (Exception e)
            {
                throw new Exception("CountryManager RemoveAll " + e);
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
    }
}
