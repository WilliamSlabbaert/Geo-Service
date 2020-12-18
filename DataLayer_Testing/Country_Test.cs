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
            temp.continentRepo.removeAll();
            temp.continentRepo.add(new Continent("test-continent"));
            Continent con = temp.continentRepo.getAll()[0];
            Assert.AreEqual("test-continent", con.Name);

            temp.countryRepo.add(new Country("test-country1",500,500,con));
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

            con = temp.continentRepo.getAll()[0];
            Assert.AreEqual(520, con.Population);

            temp.countryRepo.delete(country1.ID);

            con = temp.continentRepo.getAll()[0];
            Assert.AreEqual(20, con.Population);
            
            countries = temp.countryRepo.getAll();
            Assert.AreEqual(1, countries.Count);

            country1 = temp.countryRepo.getById(countries[0].ID);
            country1.SetPopulation(1000);
            country1.SetSurface(2000);
            country1.SetName("test2");

            temp.countryRepo.update(country1);

            countries = temp.countryRepo.getAll();
            Assert.AreEqual("test2", countries[0].Name);
            Assert.AreEqual(1000, countries[0].Population);
            Assert.AreEqual(2000, countries[0].Surface);

            temp.countryRepo.removeAll();
            temp.continentRepo.removeAll();
        }
    }
}
