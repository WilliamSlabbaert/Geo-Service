using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class ContinentManager
    {
        public IUnitOfWork uow;
        public ContinentManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void Add(Continent con)
        {
            try
            {
                if (uow.continentRepo.IfExist(con))
                    uow.continentRepo.add(con);
                else
                    throw new Exception("Continent already exist");
            }
            catch(Exception e)
            {
                throw new Exception("ContinentManager Add: " + e);
            }
        }
        public Continent Get(int id)
        {
            try
            {
                return uow.continentRepo.getById(id);
            }
            catch (Exception e)
            {
                throw new Exception("ContinentManager Get: " + e);
            }
        }
        public List<Continent> GetAll()
        {
            try
            {
                return uow.continentRepo.getAll();
            }
            catch (Exception e)
            {
                throw new Exception("ContinentManager GetAll: " + e);
            }
        }
        public bool IfExist(Continent con)
        {
            return uow.continentRepo.IfExist(con);
        }
        

        public void Update(Continent con)
        {
            try
            {
                uow.continentRepo.update(con);
            }
            catch (Exception e)
            {
                throw new Exception("ContinentManager Update: " + e);
            }
        }
        public void Remove(int id)
        {
            try
            {
                uow.continentRepo.delete(id);
            }
            catch (Exception e)
            {
                throw new Exception("ContinentManager Remove: " + e);
            }
        }
        public void RemoveAll()
        {
            try
            {
                uow.continentRepo.removeAll();
            }
            catch (Exception e)
            {
                throw new Exception("ContinentManager RemoveAll: " + e);
            }
        }
    }
}
