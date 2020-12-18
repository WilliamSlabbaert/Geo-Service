using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class City
    {
        public City()
        {
        }

        public City(string name, int population, Country country,Boolean iscapital)
        {
            SetPopulation(population);
            SetName(name);
            SetCountry(country);
            this.IsCapital = iscapital;
        }
        public void SetPopulation(int pop)
        {
            if (pop <= 0 || pop == null)
                throw new Exception("Population is lower than 0");
            this.Population = pop;
        }
        public void SetName(String name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is null");
            this.Name = name;
        }
        public void SetCountry(Country country)
        {
            if (country == null)
                throw new Exception("Country is null");
            this.Country = country;
        }

        public int ID { get; set; }
        public String Name { get; set; }
        public Boolean IsCapital { get; set; }
        public int Population { get; set; }
        public Country Country { get; set; }
    }
}
