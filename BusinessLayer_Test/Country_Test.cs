using BusinessLayer;
using BusinessLayer.Managers;
using Datalaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BusinessLayer_Test
{
    [TestClass]
    public class Country_Test
    {
        [TestMethod]
        public void Country_Manager_Test()
        {
            CountryManager tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            ContinentManager tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));
            tempCountry.RemoveAll();
            tempContinent.RemoveAll();

            tempContinent.Add(new Continent("test-Continent"));
            tempContinent.Add(new Continent("test-Continent1"));
            List<Continent> continents = tempContinent.GetAll();
            Assert.AreEqual(2, continents.Count);
            Assert.AreEqual("test-Continent", continents[0].Name);

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

            tempCountry.Update(country1, "test-Country",2000,10, continents[1]);
            countries = tempCountry.GetAll();
            country1 = tempCountry.Get(countries[0].ID);
            country2 = tempCountry.Get(countries[1].ID);

            Assert.AreEqual(2, countries.Count);
            Assert.AreEqual("test-Country", country1.Name);
            Assert.AreEqual("test-Country1", country2.Name);

            Assert.AreEqual(2000, country1.Population);
            Assert.AreEqual(50, country2.Population);

            Assert.AreEqual(10, country1.Surface);
            Assert.AreEqual(20, country2.Surface);

            tempCountry.Remove(country1.ID);

            countries = tempCountry.GetAll();
            Assert.AreEqual(1, countries.Count);

            tempCountry.RemoveAll();
            tempContinent.RemoveAll();

        }
    }
}
