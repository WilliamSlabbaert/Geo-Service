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
            this.context = context;
        }

        public void add(Continent con)
        {
            context.ContinentData.Add(con);
        }

        public void delete(int id)
        {
            context.ContinentData.Remove(getById(id));
        }

        public List<Continent> getAll()
        {
            return context.ContinentData.Include(s => s.Countries).ToList();
        }

        public Continent getById(int id)
        {
            Continent temp = context.ContinentData.Include(s => s.Countries).Where(s => s.ID == id).ToList()[0];
            if (temp == null)
                return null;
            else
                return temp;
        }

        public void removeAll()
        {
            foreach (Continent item in getAll())
                delete(item.ID);
        }

        public void update(Continent con)
        {
            context.ContinentData.Update(con);
        }
    }
}
