using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface ICountryRepo
    {
        void add(Country con);
        void delete(int id);
        List<Country> getAll();
        Country getById(int id);
        void removeAll();
        void update(Country con);
    }
}
