using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IUnitOfWork
    {
        public int Complete();
        public void Dispose();
    }
}
