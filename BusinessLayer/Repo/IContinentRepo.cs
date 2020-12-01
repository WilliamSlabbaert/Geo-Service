using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IContinentRepo
    {
        void add(Continent con);
        void delete(int id);
        List<Continent> getAll();
        Continent getById(int id);
        void removeAll();
        void update(Continent con);
    }
}
