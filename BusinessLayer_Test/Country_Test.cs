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

        }
        [TestMethod]
        public void Country_Update_Test()
        {
            var tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            var tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));

          
        }
        [TestMethod]
        public void Country_Get_Test()
        {
            var tempCountry = new CountryManager(new UnitOfWork(new DataContext("test")));
            var tempContinent = new ContinentManager(new UnitOfWork(new DataContext("test")));
        }
    }
}
