using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalaag.DataRepo
{
    class RiverRepo : IRiverRepo
    {
        private DataContext context;

        public RiverRepo(DataContext context)
        {
            this.context = context;
        }

        public void add(River con)
        {
            context.RiverData.Add(con);
        }

        public void delete(int id)
        {
            context.RiverData.Remove(getById(id));
        }

        public List<River> getAll()
        {
            return context.RiverData.Include(s => s.Countries).ToList();
        }

        public River getById(int id)
        {
            River temp = context.RiverData.Include(s => s.Countries).Where(s => s.ID == id).ToList()[0];
            if (temp == null)
                return null;
            else
                return temp;
        }

        public void removeAll()
        {
            foreach (River item in getAll())
                delete(item.ID);
        }

        public void update(River con)
        {
            context.RiverData.Update(con);
        }
    }
}
