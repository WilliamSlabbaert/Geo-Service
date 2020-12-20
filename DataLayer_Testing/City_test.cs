using BusinessLayer;
using Datalaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer_Testing
{
    [TestClass]
    public class City_test
    {
        [TestMethod]
        public void Test_Repo_City()
        {
            var temp = new UnitOfWork(new DataContext("test"));
            temp.cityRepo.removeAll();
            temp.countryRepo.removeAll();
            temp.continentRepo.removeAll();

            temp.continentRepo.add(new Continent("test-continent"));
            Continent con = temp.continentRepo.getAll()[0];
            Assert.AreEqual("test-continent", con.Name);

            temp.countryRepo.add(new Country("test-country1", 500, 500, con));
            temp.countryRepo.add(new Country("test-country", 20, 50, con));

            List<Country> countries = temp.countryRepo.getAll();
            Country country1 = temp.countryRepo.getById(countries[0].ID);
            Country country2 = temp.countryRepo.getById(countries[1].ID);

            Assert.AreEqual(2, countries.Count);

            Assert.AreEqual("test-country1", country1.Name);
            Assert.AreEqual("test-country", country2.Name);

            Assert.AreEqual(500, country1.Population);
            Assert.AreEqual(20, country2.Population);

            Assert.AreEqual(500, country1.Surface);
            Assert.AreEqual(50, country2.Surface);

            Assert.AreEqual(con, country1.Continent);
            Assert.AreEqual(con, country2.Continent);

            temp.cityRepo.add(new City("test-city",50,country1,true));
            temp.cityRepo.add(new City("test-city1", 250, country1, false));

            var cities = temp.cityRepo.getAll();
            City city = temp.cityRepo.getById(cities[0].ID);
            City city1 = temp.cityRepo.getById(cities[1].ID);

            Assert.AreEqual(city.Name, "test-city");
            Assert.AreEqual(city1.Name, "test-city1");

            Assert.AreEqual(city.Population, 50);
            Assert.AreEqual(city1.Population, 250);

            Assert.AreEqual(city.IsCapital, true);
            Assert.AreEqual(city1.IsCapital, false);

            city.SetName("test-city2");
            city.SetPopulation(30);
            city.SetCountry(country2);

            temp.cityRepo.update(city);

            cities = temp.cityRepo.getAll();
            city = temp.cityRepo.getById(cities[0].ID);

            Assert.AreEqual(2, cities.Count);
            Assert.AreEqual(city.Name, "test-city2");

            Assert.AreEqual(city.Population, 30);

            Assert.AreEqual(city.IsCapital, true);


            temp.cityRepo.delete(city.ID);
            cities = temp.cityRepo.getAll();
            Assert.AreEqual(1, cities.Count);


            temp.cityRepo.removeAll();
            temp.countryRepo.removeAll();
            temp.continentRepo.removeAll();
        }
    }
}
