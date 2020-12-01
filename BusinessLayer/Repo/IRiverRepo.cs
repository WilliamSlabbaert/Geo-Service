using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IRiverRepo
    {
        void add(River con);
        void delete(int id);
        List<River> getAll();
        River getById(int id);
        void removeAll();
        void update(River con);
    }
}
