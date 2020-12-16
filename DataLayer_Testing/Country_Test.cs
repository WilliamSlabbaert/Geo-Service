using BusinessLayer;
using Datalaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer_Testing
{
    [TestClass]
    public class Country_Test
    {
        [TestMethod]
        public void Test_Repo_Country()
        {
            var temp = new UnitOfWork(new DataContext("test"));
            temp.countryRepo.removeAll();
            temp.Complete();
            temp.continentRepo.removeAll();
            temp.Complete();
            temp.continentRepo.add(new Continent("test-continent"));
            temp.Complete();
            var continents = temp.continentRepo.getAll();

            temp.countryRepo.add(new Country("test-country", 500, 550, continents[0]));
            temp.Complete();
            var countries = temp.countryRepo.getAll();

            Assert.AreEqual("test-country", countries[0].Name);
            Assert.AreEqual(500, countries[0].Population);
            Assert.AreEqual(550, countries[0].Surface);

            temp.countryRepo.removeAll();
            temp.Complete();
            temp.continentRepo.removeAll();
            temp.Complete();

            temp = new UnitOfWork(new DataContext("test"));

            temp.continentRepo.add(new Continent("test-continent"));
            temp.Complete();
            continents = temp.continentRepo.getAll();
            temp.countryRepo.add(new Country("test-country1", 500, 550, continents[0]));
            temp.Complete();
            temp.countryRepo.add(new Country("test-country2", 500, 550, continents[0]));
            temp.Complete();
            temp.countryRepo.add(new Country("test-country3", 500, 550, continents[0]));
            temp.Complete();

            countries = temp.countryRepo.getAll();
            var id = countries[0].ID;

            var country = temp.countryRepo.getById(id);

            Assert.AreEqual("test-country1", country.Name);
            Assert.AreEqual(500, country.Population);
            Assert.AreEqual(550, country.Surface);

            country.SetName("test");
            country.SetPopulation(5);
            country.SetSurface(60);
            temp.countryRepo.update(country);
            temp.Complete();

            country = temp.countryRepo.getById(id);
            Assert.AreEqual("test", country.Name);
            Assert.AreEqual(5, country.Population);
            Assert.AreEqual(60, country.Surface);

            countries = temp.countryRepo.getAll();
            Assert.AreEqual(3, countries.Count);

            temp.countryRepo.delete(id);
            temp.Complete();

            countries = temp.countryRepo.getAll();
            Assert.AreEqual(2, countries.Count);

            temp.countryRepo.removeAll();
            temp.Complete();
            temp.continentRepo.removeAll();
            temp.Complete();
        }
    }
}
