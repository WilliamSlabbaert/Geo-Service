using BusinessLayer;
using Datalaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer_Testing
{
    [TestClass]
    public class River_Test
    {
        [TestMethod]
        public void Test_Repo_River()
        {
            UnitOfWork temp = new UnitOfWork(new DataContext("test"));
            temp.riverRepo.removeAll();
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

            var river = new River("test-river", 500, countries);
            temp.riverRepo.add(river);
       
            temp.countryRepo.add(new Country("test-country2", 5002, 5002, con));
            countries = temp.countryRepo.getAll();
            country1 = temp.countryRepo.getById(countries[0].ID);
            country2 = temp.countryRepo.getById(countries[1].ID);

            Assert.AreEqual(3, countries.Count);
            Assert.AreEqual(river, country1.Rivers[0]);
            Assert.AreEqual(river, country2.Rivers[0]);
            var rivers = temp.riverRepo.getAll();
            river = temp.riverRepo.getById(rivers[0].ID);
            Assert.AreEqual(2, river.Countries.Count);
            rivers[0].SetLenght(5000);
            rivers[0].SetName("test");
            rivers[0].AddCountry(countries[2]);

            temp.riverRepo.update(rivers[0]);
            temp.riverRepo.delete(rivers[0].ID);

            rivers = temp.riverRepo.getAll();
            Assert.AreEqual(0, rivers.Count);

            temp.riverRepo.removeAll();
            temp.countryRepo.removeAll();
            temp.continentRepo.removeAll();
        }
    }
}
