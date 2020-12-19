using BusinessLayer;
using BusinessLayer.Managers;
using Datalaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLayer_Test
{
    [TestClass]
    public class Continent_Test
    {
        [TestMethod]
        public void Continent_Manager_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));
            temp.RemoveAll();
            Continent con = new Continent("test-continent");
            Assert.AreEqual(0, con.ID);
            temp.Add(con);
            temp.Add(new Continent("test-continent1"));

            List<Continent> continents = temp.GetAll();
            Continent continent1 = temp.Get(continents[0].ID);
            Continent continent2 = temp.Get(continents[1].ID);

            Assert.AreEqual(2, continents.Count);
            Assert.AreEqual("test-continent",continent1.Name);
            Assert.AreEqual("test-continent1", continent2.Name);

            temp.Remove(continent1.ID);

            continents = temp.GetAll();
            continent1 = temp.Get(continents[0].ID);

            Assert.AreEqual(1, continents.Count);
            Assert.AreEqual("test-continent1", continent1.Name);

            continent1.SetName("test");

            temp.Update(continent1);

            continents = temp.GetAll();
            continent1 = temp.Get(continents[0].ID);
            Assert.AreEqual("test", continent1.Name);
            temp.RemoveAll();
        }
       
    }
}
