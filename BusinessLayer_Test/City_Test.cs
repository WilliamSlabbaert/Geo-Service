using BusinessLayer;
using BusinessLayer.Managers;
using Datalaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer_Test
{
    [TestClass]
    public class City_Test
    {
        [TestMethod]
        public void City_Manager_Test()
        {
            CityManager tempCity = new CityManager(new UnitOfWork(new DataContext("test")));
            CountryManager tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            ContinentManager tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));
            tempCity.RemoveAll();
            tempCountry.RemoveAll();
            tempContinent.RemoveAll();

            tempContinent.Add(new Continent("test-Continent"));
            tempContinent.Add(new Continent("test-Continent1"));
            List<Continent> continents = tempContinent.GetAll();
            Assert.AreEqual(2, continents.Count);

            tempCountry.Add(new Country("test", 500, 200, continents[0]));
            tempCountry.Add(new Country("test-Country1", 50, 20, continents[0]));

            List<Country> countries = tempCountry.GetAll();
            Country country1 = tempCountry.Get(countries[0].ID);
            Country country2 = tempCountry.Get(countries[1].ID);

            Assert.AreEqual(2, countries.Count);
            Assert.AreEqual("test", country1.Name);
            Assert.AreEqual("test-Country1", country2.Name);

            Assert.AreEqual(500, country1.Population);
            Assert.AreEqual(50, country2.Population);

            Assert.AreEqual(200, country1.Surface);
            Assert.AreEqual(20, country2.Surface);

            tempCity.Add(new City("test-city", 50, country1, true));
            tempCity.Add(new City("test-city1", 150, country1, false));
            tempCity.Add(new City("test-city2", 200, country1, false));
            tempCity.Add(new City("test-city3", 10, country1, false));

            var cities = tempCity.GetAll();
            City city = tempCity.Get(cities[0].ID);
            City city1 = tempCity.Get(cities[1].ID);
            City city2 = tempCity.Get(cities[2].ID);
            City city3 = tempCity.Get(cities[3].ID);

            Assert.AreEqual(city.Name, "test-city");
            Assert.AreEqual(city1.Name, "test-city1");
            Assert.AreEqual(city2.Name, "test-city2");
            Assert.AreEqual(city3.Name, "test-city3");

            Assert.AreEqual(city.IsCapital, true);
            Assert.AreEqual(city1.IsCapital, false);
            Assert.AreEqual(city2.IsCapital, false);
            Assert.AreEqual(city3.IsCapital, false);

            Assert.AreEqual(city.Population, 50);
            Assert.AreEqual(city1.Population, 150);
            Assert.AreEqual(city2.Population, 200);
            Assert.AreEqual(city3.Population, 10);

            tempCity.Update(city3, "test", 50, false, country2);

            cities = tempCity.GetAll();
            city = tempCity.Get(cities[0].ID);
            city1 = tempCity.Get(cities[1].ID);
            city2 = tempCity.Get(cities[2].ID);
            city3 = tempCity.Get(cities[3].ID);

            Assert.AreEqual(4, cities.Count);
            Assert.AreEqual(city.Name, "test-city");
            Assert.AreEqual(city1.Name, "test-city1");
            Assert.AreEqual(city2.Name, "test-city2");
            Assert.AreEqual(city3.Name, "test");

            Assert.AreEqual(city.IsCapital, true);
            Assert.AreEqual(city1.IsCapital, false);
            Assert.AreEqual(city2.IsCapital, false);
            Assert.AreEqual(city3.IsCapital, false);

            Assert.AreEqual(city.Population, 50);
            Assert.AreEqual(city1.Population, 150);
            Assert.AreEqual(city2.Population, 200);
            Assert.AreEqual(city3.Population, 50);

            tempCity.Remove(city.ID);
            cities = tempCity.GetAll();
            Assert.AreEqual(3, cities.Count);
            tempCountry.Update(country1, country1.Name, 350, 250, country1.Continent);


            tempCity = new CityManager(new UnitOfWork(new DataContext("test")));
            tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));
            tempCity.RemoveAll();
            tempCountry.RemoveAll();
            tempContinent.RemoveAll();

        }
    }
}
