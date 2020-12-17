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
            var tempManager = new UnitOfWork(new DataContext("test"));
            tempManager.cityRepo.removeAll();

            tempManager.countryRepo.removeAll();

            tempManager.continentRepo.removeAll();

            tempManager.continentRepo.add(new BusinessLayer.Continent("test-continent"));

            var continents = tempManager.continentRepo.getAll();
            Assert.AreEqual("test-continent", continents[0].Name);

            tempManager.countryRepo.add(new BusinessLayer.Country("test-country", 0, 550, continents[0]));
            tempManager.Complete();

            var countries = tempManager.countryRepo.getAll();
            Assert.AreEqual("test-country", countries[0].Name);
            Assert.AreEqual(0, countries[0].Population);
            Assert.AreEqual(550, countries[0].Surface);
            Assert.AreEqual(continents[0], countries[0].Continent);

            tempManager.cityRepo.add(new BusinessLayer.City("test-City", 5000, countries[0]));
            tempManager.cityRepo.add(new BusinessLayer.City("test-City1", 3000, countries[0]));
            tempManager.cityRepo.add(new BusinessLayer.City("test-City2", 2000, countries[0]));

            var cities = tempManager.cityRepo.getAll();
            Assert.AreEqual("test-City", cities[0].Name);
            Assert.AreEqual("test-City1", cities[1].Name);
            Assert.AreEqual("test-City2", cities[2].Name);

            Assert.AreEqual(5000, cities[0].Population);
            Assert.AreEqual(3000, cities[1].Population);
            Assert.AreEqual(2000, cities[2].Population);

            Assert.AreEqual(countries[0], cities[0].Country);
            Assert.AreEqual(countries[0], cities[1].Country);
            Assert.AreEqual(countries[0], cities[2].Country);

            tempManager.cityRepo.delete(cities[1].ID);
            
            cities = tempManager.cityRepo.getAll();
            Assert.AreEqual("test-City", cities[0].Name);
            Assert.AreEqual("test-City2", cities[1].Name);

            Assert.AreEqual(5000, cities[0].Population);
            Assert.AreEqual(2000, cities[1].Population);

            Assert.AreEqual(countries[0], cities[0].Country);
            Assert.AreEqual(countries[0], cities[1].Country);

            cities[0].SetPopulation(50);
            cities[0].SetName("test-City1");

            tempManager.cityRepo.update(cities[0]);

            cities = tempManager.cityRepo.getAll();
            var city1 = tempManager.cityRepo.getById(cities[0].ID);
            var city2 = tempManager.cityRepo.getById(cities[1].ID);
            Assert.AreEqual("test-City1", city1.Name);
            Assert.AreEqual("test-City2", city2.Name);

            Assert.AreEqual(50, city1.Population);
            Assert.AreEqual(2000, city2.Population);

            Assert.AreEqual(countries[0], city1.Country);
            Assert.AreEqual(countries[0], city2.Country);

            tempManager.cityRepo.removeAll();

            tempManager.countryRepo.removeAll();

            tempManager.continentRepo.removeAll();
        }
        [TestMethod]
        public void Test_Repo_City_Update()
        {
            var tempManager = new UnitOfWork(new DataContext("test"));
            tempManager.cityRepo.removeAll();

            tempManager.countryRepo.removeAll();

            tempManager.continentRepo.removeAll();

            tempManager.continentRepo.add(new BusinessLayer.Continent("test-continent"));

            var continents = tempManager.continentRepo.getAll();
            Assert.AreEqual("test-continent", continents[0].Name);

            tempManager.countryRepo.add(new BusinessLayer.Country("test-country", 0, 550, continents[0]));
            tempManager.Complete();
            tempManager.countryRepo.add(new BusinessLayer.Country("test-country1", 0, 5550, continents[0]));
            tempManager.Complete();

            var countries = tempManager.countryRepo.getAll();
            Assert.AreEqual("test-country", countries[0].Name);
            Assert.AreEqual(0, countries[0].Population);
            Assert.AreEqual(550, countries[0].Surface);
            Assert.AreEqual(continents[0], countries[0].Continent);

            tempManager.cityRepo.add(new BusinessLayer.City("test-City", 5000, countries[0]));
            tempManager.cityRepo.add(new BusinessLayer.City("test-City1", 3000, countries[0]));
            tempManager.cityRepo.add(new BusinessLayer.City("test-City2", 2000, countries[0]));

            var cities = tempManager.cityRepo.getAll();
            Assert.AreEqual("test-City", cities[0].Name);
            Assert.AreEqual("test-City1", cities[1].Name);
            Assert.AreEqual("test-City2", cities[2].Name);

            Assert.AreEqual(5000, cities[0].Population);
            Assert.AreEqual(3000, cities[1].Population);
            Assert.AreEqual(2000, cities[2].Population);

            Assert.AreEqual(countries[0], cities[0].Country);
            Assert.AreEqual(countries[0], cities[1].Country);
            Assert.AreEqual(countries[0], cities[2].Country);

            cities[0].SetPopulation(50);
            cities[0].SetName("test-City1");
            cities[0].SetCountry(countries[1]);

            tempManager.cityRepo.update(cities[0]);

            tempManager.cityRepo.removeAll();

            tempManager.countryRepo.removeAll();

            tempManager.continentRepo.removeAll();
        }
    }
}
