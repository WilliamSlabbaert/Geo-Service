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
        public void AddCountry(Country con)
        {
            try
            {
                uow.countryRepo.add(con);
                var tempContinent = uow.continentRepo.getById(con.Continent.ID);
                tempContinent.GetCountiesPopulationCount();
                uow.continentRepo.update(tempContinent);

                uow.Complete();
            }
            catch(Exception e)
            {
                throw new Exception("Something went wrong : (CountryManager AddCountry)" +e);
            }
        }
        public List<Country> GetAllCountries()
        {
            try
            {
                return uow.countryRepo.getAll();
            }
            catch
            {
                throw new Exception("Something went wrong : (CountryManager GetAllCountries)");
            }
        }
        public Country GetCountry(int id)
        {
            try
            {
                return uow.countryRepo.getById(id);
            }
            catch
            {
                throw new Exception("Something went wrong : (CountryManager GetCountry)");
            }
        }
        public void RemoveCountry(int id)
        {
            try
            {
                if (uow.countryRepo.getById(id).Cities.Count.Equals(0))
                {
                    uow.countryRepo.delete(id);
                    var tempContinent = uow.continentRepo.getById(GetCountry(id).Continent.ID);
                    tempContinent.GetCountiesPopulationCount();
                    uow.continentRepo.update(tempContinent);
                    uow.Complete();
                }
                else
                    throw new Exception("Country has existing cities");
            }
            catch
            {
                throw new Exception("Something went wrong : (CountryManager RemoveCountry)");
            }

        }
        public void RemoveAllCountries()
        {
            try
            {
                var temp = uow.countryRepo.getAll().SelectMany(s => s.Cities).ToList();

                if (temp.Count.Equals(0))
                {
                    uow.countryRepo.removeAll();
                    uow.Complete();
                    foreach (var item in uow.continentRepo.getAll())
                    {
                        item.GetCountiesPopulationCount();
                        uow.continentRepo.update(item);
                        uow.Complete();
                    }
                }
                else
                    throw new Exception("Countries has existing cities");
            }
            catch(Exception e)
            {
                throw new Exception("Something went wrong : (CountryManager RemoveAllCountries) " + e);
            }

        }
        public void UpdateCountry(Country con)
        {
            try
            {
                uow.countryRepo.update(con);
                uow.Complete();

                var tempContinent = uow.continentRepo.getById(con.Continent.ID);
                tempContinent.GetCountiesPopulationCount();
                uow.continentRepo.update(tempContinent);
                uow.Complete();

                tempContinent = uow.continentRepo.getById(con.Continent.ID);
            }
            catch
            {
                throw new Exception("Something went wrong : (CountryManager UpdateCountry)");
            }
        }
    }
}
