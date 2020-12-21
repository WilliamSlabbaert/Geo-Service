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
    public class River_Test
    {
        [TestMethod]
        public void River_Manager_Test()
        {
            CountryManager tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            ContinentManager tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));
            RiverManager tempRiver = new RiverManager(new UnitOfWork(new DataContext("test")));
            tempRiver.RemoveAll();
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

            tempRiver.Add(new River("test-river",250,countries));

            var rivers = tempRiver.GetAll();
            var river = tempRiver.Get(rivers[0].ID);

            Assert.AreEqual("test-river", river.Name);
            Assert.AreEqual(250, river.Lenght);
            country1 = tempCountry.Get(countries[0].ID);
            Assert.AreEqual(1, country1.Rivers.Count);

            Assert.AreEqual(2, river.Countries.Count);
            Assert.AreEqual("test", river.Countries[0].Name);
            Assert.AreEqual("test-Country1", river.Countries[1].Name);

            Assert.AreEqual(500, river.Countries[0].Population);
            Assert.AreEqual(50, river.Countries[1].Population);

            Assert.AreEqual(200, river.Countries[0].Surface);
            Assert.AreEqual(20, river.Countries[1].Surface);

            tempRiver.Update(river, "test", 50, countries);

            rivers = tempRiver.GetAll();
            river = tempRiver.Get(rivers[0].ID);

            Assert.AreEqual("test", river.Name);
            Assert.AreEqual(50, river.Lenght);

            tempRiver.Remove(river.ID);
            rivers = tempRiver.GetAll();
            Assert.AreEqual(0, rivers.Count);
            tempRiver.RemoveAll();
            tempCountry.RemoveAll();
            tempContinent.RemoveAll();
        }
    }
}
