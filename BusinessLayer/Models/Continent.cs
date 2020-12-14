using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Continent
    {
        public Continent()
        {
        }

        public Continent(string name)
        {
            SetName(name);
            SetPopulation(0);
        }
        public void AddCountry(Country con)
        {
            if (con.Equals(null))
                throw new Exception("Country is Null");
            this.Countries.Add(con);
        }

        public void SetPopulation(int pop) 
        {
            if(pop <0 || pop == null)
                throw new Exception("Population is lower than 0");
            this.Population = pop;
        }
        public void SetName(String name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is null");
            this.Name = name;
        }
        public int ID { get; set; }
        public String Name { get; private set; }
        public int Population { get; private set; }
        public virtual List<Country> Countries { get; set; }
    }
}
