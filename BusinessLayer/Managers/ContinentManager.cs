using System;
using System.Collections.Generic;
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
        public List<Continent> GetAllContinents()
        {
            try
            {
                return uow.continentRepo.getAll();
            }
            catch
            {
                throw new Exception("Something went wrong : (ContinentManager GetAllContinents)");
            }
        }
        public Continent GetContinent(int id)
        {
            try
            {
                return uow.continentRepo.getById(id);
            }
            catch
            {
                throw new Exception("Something went wrong : (ContinentManager GetContinent)");
            }
        }
        public void AddContinent(Continent con)
        {
            try
            {
                uow.continentRepo.add(con);
                uow.Complete();
            }catch{
                throw new Exception("Something went wrong : (ContinentManager AddContinent)");
            }

        }
        public void RemoveContinent(int id)
        {
            try
            {
                uow.continentRepo.delete(id);
                uow.Complete();
            }catch
            {
                throw new Exception("Something went wrong : (ContinentManager RemoveContinent)");
            }
            
        }
        public void RemoveAllContinents()
        {
            try
            {
                uow.continentRepo.removeAll();
                uow.Complete();
            }
            catch
            {
                throw new Exception("Something went wrong : (ContinentManager RemoveAllContinents)");
            }

        }
        public void UpdateContinent(Continent con)
        {
            try
            {
                uow.continentRepo.update(con);
                uow.Complete();
            }
            catch
            {
                throw new Exception("Something went wrong : (ContinentManager UpdateContinent)");
            }
        }

    }
}
