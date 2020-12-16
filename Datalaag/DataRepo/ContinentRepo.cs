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
            catch
            {
                throw new Exception("Something went wrong : ContinentRepo add");
            }
        }

        public void delete(int id)
        {
            try
            {
                context.ContinentData.Remove(getById(id));
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Something went wrong : ContinentRepo add");
            }
        }

        public List<Continent> getAll()
        {
            try
            {
                var temp = context.ContinentData.Include(s => s.Countries).ToList();
                foreach (var t in temp)
                    t.GetCountiesPopulationCount();
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
                Continent temp = context.ContinentData.Include(s => s.Countries).Where(s => s.ID == id).ToList()[0];
                if (temp == null)
                    return null;
                else
                    return temp;
            }
            catch
            {
                throw new Exception("Something went wrong : ContinentRepo getById");
            }
        }

        public void removeAll()
        {
            try
            {
                foreach (Continent item in getAll())
                    delete(item.ID);
            }
            catch
            {
                throw new Exception("Something went wrong : ContinentRepo removeAll");
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
    }
}
