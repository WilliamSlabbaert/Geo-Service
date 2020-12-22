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
            try
            {
                context.RiverData.Add(con);
                context.SaveChanges();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public void delete(int id)
        {
            context.RiverData.Remove(getById(id));
            context.SaveChanges();
        }

        public List<River> getAll()
        {
            return context.RiverData.ToList();
        }

        public River getById(int id)
        {
            River temp = context.RiverData.FirstOrDefault(s => s.ID == id);
            if (temp == null)
                throw new Exception("River not found");
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
            context.SaveChanges();
        }
    }
}
