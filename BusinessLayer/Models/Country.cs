using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Country
    {
        public Country()
        {
        }

        public Country(string name, int population, double surface, Continent continent)
        {
            SetPopulation(population);
            SetName(name);
            SetSurface(surface);
            SetContinent(continent);
        }
        public void GetCountiesPopulationCount()
        {
            foreach (var city in this.Cities)
                this.Population += city.Population;
        }
        public void SetPopulation(int pop)
        {
            if (pop <= 0 )
                throw new Exception("Population is lower than 0");
            this.Population = pop;
        }
        public void SetName(String name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is null");
            this.Name = name;
        }
        public void SetSurface(Double surface)
        {
            if (surface <= 0 )
                throw new Exception("Surface is lower than 0");
            this.Surface = surface;
        }
        public void SetContinent(Continent continent)
        {
            if (continent == null)
                throw new Exception("Continent is null");
            this.Continent = continent;
        }
        public void GetCitiesPopulationCount()
        {
            this.Population = 0;
            foreach (var city in this.Cities)
                this.Population = this.Population + city.Population;
        }
        public int ID { get; set; }
        public String Name { get; set; }
        public int Population { get; set; }
        public Double Surface { get; set; }
        public Continent Continent { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
        public List<River> Rivers { get; set; } = new List<River>();
    }
}
