using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Managers
{
    public class CityManager
    {
        public IUnitOfWork uow;
        public CityManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void Add(City con)
        {
            try
            {
                if (CalculatePopulationCheck(con))
                {
                    uow.cityRepo.update(con);
                    uow.Complete();
                }
                else
                    throw new Exception("City population is too high for country");
            }
            catch(Exception e)
            {
                throw new Exception("CityManager Add: " + e);
            }
        }
        public City Get(int id)
        {
            try
            {
                return uow.cityRepo.getById(id);
            }
            catch(Exception e)
            {
                throw new Exception("CityManager Get: " + e);
            }
        }
        public List<City> GetAll()
        {
            try
            {
                return uow.cityRepo.getAll();
            }
            catch (Exception e)
            {
                throw new Exception("CityManager GetAll: " + e);
            }
        }
        public void Remove(int id)
        {
            try
            {
                uow.cityRepo.delete(id);
            }
            catch (Exception e)
            {
                throw new Exception("CityManager RemoveAll: " + e);
            }
        }
        public void RemoveAll()
        {
            try
            {
                uow.cityRepo.removeAll();
            }
            catch (Exception e)
            {
                throw new Exception("CityManager RemoveAll: " + e);
            }
        }
        public void Update(City city,String name,int population,bool capital,Country con)
        {
            try
            {
                var temp = city;
                city.SetCountry(con);
                city.SetName(name);
                city.SetPopulation(population);
                city.IsCapital = capital;
                if (CalculatePopulationCheck(city))
                    uow.Complete();
                else
                    throw new Exception("City population is too high for country");
            }
            catch (Exception e)
            {
                throw new Exception("CityManager RemoveAll: " + e);
            }
        }
        public bool CalculatePopulationCheck(City city)
        {
            var tempList = uow.cityRepo.getAll().FindAll(c => c.Country == city.Country);
            tempList.Remove(city);
            int totalPopulation = 0;
            foreach (var cityItem in tempList)
                totalPopulation += cityItem.Population;
            totalPopulation += city.Population;
            if (totalPopulation <= city.Country.Population)
                return true;
            return false;
        }
    }
}
