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
            catch(Exception e)
            {
                throw new Exception("Something went wrong : (ContinentManager GetContinent) " + e);
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
                if (uow.continentRepo.getById(id).Countries.Count.Equals(0))
                {
                    uow.continentRepo.delete(id);
                    uow.Complete();
                }
                else
                    throw new Exception("Continent still has existing countries");
            }catch
            {
                throw new Exception("Something went wrong : (ContinentManager RemoveContinent)");
            }
            
        }
        public void RemoveAllContinents()
        {
            try
            {
                var temp = uow.continentRepo.getAll().SelectMany(s => s.Countries).ToList();
                //var temp = uow.continentRepo.getAll();
                //int count = 0;
                //foreach(var i in temp)
                //    count += i.Countries.Count;

                if (temp.Count == 0)
                {
                    uow.continentRepo.removeAll();
                    uow.Complete();
                }
                else
                    throw new Exception("Continents still have existing countries");
            }
            catch(Exception e)
            {
                throw new Exception("Something went wrong : (ContinentManager RemoveAllContinents) => " + e);
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
