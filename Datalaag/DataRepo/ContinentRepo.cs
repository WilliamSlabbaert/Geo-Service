using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalaag
{
    public class ContinentRepo : IContinentRepo
    {
        private DataContext context;

        public ContinentRepo(DataContext context)
        {
            try
            {
                this.context = context;
            }
            catch
            {
                throw new Exception("Something went wrong : ContinentRepo");
            }
        }

        public void add(Continent con)
        {
            try
            {

                context.ContinentData.Add(con);
                context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong : ContinentRepo add " + e);
            }
        }

        public void delete(int id)
        {
            try
            {
                if (IfContainsCountries(id))
                {
                    context.ContinentData.Remove(getById(id));
                    context.SaveChanges();
                }
                else
                    throw new Exception("Continent still have countries");
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong : ContinentRepo delete " + e);
            }
        }
        public Boolean IfContainsCountries(int id)
        {
            var temp = context.CountryData.ToList().FindAll(s => s.Continent == getById(id));
            if (temp.Count == 0 || temp == null)
                return true;
            return false;
        }

        public List<Continent> getAll()
        {
            try
            {
                var temp = context.ContinentData.ToList();

                return temp;
            }
            catch
            {
                throw new Exception("Something went wrong : ContinentRepo getAll");
            }
        }

        public Continent getById(int id)
        {
            try
            {
                Continent temp = context.ContinentData.FirstOrDefault(s => s.ID == id);
                if (temp == null)
                    throw new Exception("Continent not found");
                else
                    return temp;
            }
            catch(Exception e)
            {
                throw new Exception("Something went wrong : ContinentRepo getById "+ e.Message);
            }
        }

        public void removeAll()
        {
            try
            {
                foreach (Continent item in getAll())
                    delete(item.ID);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong : ContinentRepo removeAll " + e);
            }
        }

        public void update(Continent con)
        {
            try
            {
                context.ContinentData.Update(con);
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Something went wrong : ContinentRepo update");
            }
        }
        public bool IfExist(Continent con)
        {
            var temp = getAll().FirstOrDefault(s => s.Name == con.Name);
            if (temp == null)
                return true;
            return false;
        }
    }
}
