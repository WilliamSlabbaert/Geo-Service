using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Managers
{
    public class RiverManager
    {
        public IUnitOfWork uow;
        public RiverManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void AddRiver(River con)
        {
            uow.riverRepo.add(con);
            uow.Complete();
        }
    }
}
