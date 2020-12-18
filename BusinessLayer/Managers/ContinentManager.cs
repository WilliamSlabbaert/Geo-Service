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
    }
}
