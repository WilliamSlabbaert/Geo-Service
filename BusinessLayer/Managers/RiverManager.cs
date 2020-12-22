using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Managers
{
    public class RiverManager
    {
        public IUnitOfWork uow;
        public RiverManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void Add(River con)
        {
            try
            {
                var tempList = con.Countries.Select(s => s.ID).ToList();
                List<Country> countryList =  uow.countryRepo.getAll().FindAll(s => tempList.Contains(s.ID));
                con.SetCountries(countryList);
                uow.riverRepo.add(con);
                uow.Complete();
            }catch(Exception e)
            {
                throw new Exception("RiverManager Add " + e);
            }

        }
        public River Get(int id)
        {
            try
            {
                return uow.riverRepo.getById(id);
            }
            catch (Exception e)
            {
                throw new Exception("RiverManager Get " + e);
            }
        }
        public List<River> GetAll()
        {
            try
            {
                return uow.riverRepo.getAll();
            }
            catch (Exception e)
            {
                throw new Exception("RiverManager GetAll " + e);
            }
        }
        public void Remove(int id)
        {
            try
            {
                uow.riverRepo.delete(id);
                uow.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("RiverManager Remove " + e);
            }
        }
        public void RemoveAll()
        {
            try
            {
                uow.riverRepo.removeAll();
                uow.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("RiverManager RemoveAll " + e);
            }
        }
        public void Update(River river, String name, Double lenght, List<Country> countries)
        {
            try
            {
                var tempList = countries.Select(s => s.ID).ToList();
                List<Country> countryList = uow.countryRepo.getAll().FindAll(s => tempList.Contains(s.ID));

                river.SetLenght(lenght);
                river.SetName(name);
                river.SetCountries(countryList);
                uow.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("RiverManager Update " + e.Message);
            }
        }
    }
}
