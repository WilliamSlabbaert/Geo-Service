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
                uow.riverRepo.update(con);
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
                river.SetLenght(lenght);
                river.SetName(name);
                river.SetCountries(countries);
                uow.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("RiverManager Update " + e);
            }
        }
    }
}
