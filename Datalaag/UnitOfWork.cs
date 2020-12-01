using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datalaag
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }
        public int Complete()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
