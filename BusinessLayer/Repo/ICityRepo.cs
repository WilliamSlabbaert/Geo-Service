using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface ICityRepo
    {
        void add(City city);
        void delete(int id);
        List<City> getAll();
        City getById(int id);
        void removeAll();
        void update(City city);
        List<City> getAllByCountry(Country country);
    }
}
