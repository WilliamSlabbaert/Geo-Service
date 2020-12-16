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
        public void Country_Add_Test()
        {
            var tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            var tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));
            tempCountry.RemoveAllCountries();
            tempContinent.RemoveAllContinents();

            tempContinent.AddContinent(new Continent("test-Continent"));
            var continents = tempContinent.GetAllContinents();
            Assert.AreEqual("test-Continent", continents[0].Name);
            tempCountry.AddCountry(new Country("test-Country", 500, 550, continents[0]));

            var countries = tempCountry.GetAllCountries();

            Assert.AreEqual("test-Country", countries[0].Name);
            Assert.AreEqual(500, countries[0].Population);

            tempCountry.RemoveAllCountries();
            tempContinent.RemoveAllContinents();
        }
        [TestMethod]
        public void Country_Update_Test()
        {
            var tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            var tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));

            tempCountry.RemoveAllCountries();
            tempContinent.RemoveAllContinents();

            tempContinent.AddContinent(new Continent("test-Continent"));
            var continents = tempContinent.GetAllContinents();
            Assert.AreEqual("test-Continent", continents[0].Name);
            tempCountry.AddCountry(new Country("test-Country", 500, 550, continents[0]));

            var countries = tempCountry.GetAllCountries();

            Assert.AreEqual("test-Country", countries[0].Name);
            Assert.AreEqual(500, countries[0].Population);

            countries[0].SetName("test-Country1");
            countries[0].SetPopulation(50);
            tempCountry.UpdateCountry(countries[0]);


            countries = tempCountry.GetAllCountries();
            Assert.AreEqual("test-Country1", countries[0].Name);
            Assert.AreEqual(50, countries[0].Population);

            var tempcontinents = tempContinent.GetAllContinents();

            Assert.AreEqual("test-Continent", tempcontinents[0].Name);
            Assert.AreEqual("test-Country1", tempcontinents[0].Countries[0].Name);

            tempcontinents[0].GetCountiesPopulationCount();
            Assert.AreEqual(50, tempcontinents[0].Population);

            tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));
            tempCountry.RemoveAllCountries();
            tempContinent.RemoveAllContinents();

        }
        [TestMethod]
        public void Country_Get_Test()
        {
            var tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            var tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));

            tempCountry.RemoveAllCountries();
            tempContinent.RemoveAllContinents();

            tempContinent.AddContinent(new Continent("test-Continent"));
            var continents = tempContinent.GetAllContinents();
            Assert.AreEqual("test-Continent", continents[0].Name);
            tempCountry.AddCountry(new Country("test-Country", 500, 550, continents[0]));
            tempCountry.AddCountry(new Country("test-Country1", 500, 550, continents[0]));

            var countries = tempCountry.GetAllCountries();
            var country = tempCountry.GetCountry(countries[0].ID);

            Assert.AreEqual("test-Country", country.Name);
            Assert.AreEqual(500, country.Population);

            country = tempCountry.GetCountry(countries[1].ID);
            Assert.AreEqual("test-Country1", country.Name);
            Assert.AreEqual(500, country.Population);

            var tempcontinents = tempContinent.GetAllContinents();
            
            Assert.AreEqual(1000, tempcontinents[0].Population);

            tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));
            tempCountry.RemoveAllCountries();
            tempContinent.RemoveAllContinents();
        }
    }
}
