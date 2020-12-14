using BusinessLayer;
using BusinessLayer.Managers;
using Datalaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLayer_Test
{
    [TestClass]
    public class Continent_Test
    {
        [TestMethod]
        public void Continent_Add_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));
            temp.RemoveAllContinents();
            var continent = new Continent("test-Continent");
            Assert.IsNotNull(continent);
            Assert.AreEqual("test-Continent", continent.Name);
            Assert.AreEqual(0, continent.Population);

            temp.AddContinent(continent);
            var continents = temp.GetAllContinents();

            Assert.IsNotNull(continents[0]);
            Assert.AreEqual("test-Continent", continents[0].Name);
            Assert.AreEqual(0, continents[0].Population);

            temp.RemoveAllContinents();
        }
        [TestMethod]
        public void Continent_Get_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));
            temp.RemoveAllContinents();
            var continent = new Continent("test-Continent");
            Assert.IsNotNull(continent);
            Assert.AreEqual("test-Continent", continent.Name);
            Assert.AreEqual(0, continent.Population);

            temp.AddContinent(continent);
            var continents = temp.GetAllContinents();
            int id = continents[0].ID;

            continent = temp.GetContinent(id);

            Assert.IsNotNull(continent);
            Assert.AreEqual("test-Continent", continent.Name);
            Assert.AreEqual(0, continent.Population);

            temp.RemoveAllContinents();
        }
        [TestMethod]
        public void Continent_Delete_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));
            temp.RemoveAllContinents();
            var continent = new Continent("test-Continent");
            Assert.IsNotNull(continent);
            Assert.AreEqual("test-Continent", continent.Name);
            Assert.AreEqual(0, continent.Population);

            temp.AddContinent(continent);
            var continents = temp.GetAllContinents();
            int id = continents[0].ID;

            continent = temp.GetContinent(id);

            Assert.IsNotNull(continent);
            Assert.AreEqual("test-Continent", continent.Name);
            Assert.AreEqual(0, continent.Population);

            temp.RemoveContinent(id);
            continents = temp.GetAllContinents();

            Assert.AreEqual(0, continents.Count);
        }
        [TestMethod]
        public void Continent_GetAll_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));
            temp.RemoveAllContinents();
            var continent1 = new Continent("test-Continent1");
            var continent2 = new Continent("test-Continent2");
            var continent3 = new Continent("test-Continent3");

            Assert.IsNotNull(continent1);
            Assert.AreEqual("test-Continent1", continent1.Name);
            Assert.AreEqual(0, continent1.Population);

            Assert.IsNotNull(continent2);
            Assert.AreEqual("test-Continent2", continent2.Name);
            Assert.AreEqual(0, continent2.Population);

            Assert.IsNotNull(continent3);
            Assert.AreEqual("test-Continent3", continent3.Name);
            Assert.AreEqual(0, continent3.Population);

            temp.AddContinent(continent1);
            temp.AddContinent(continent2);
            temp.AddContinent(continent3);

            var continents = temp.GetAllContinents();

            Assert.AreEqual(3, continents.Count);

            Assert.IsNotNull(continents[0]);
            Assert.AreEqual("test-Continent1", continents[0].Name);
            Assert.AreEqual(0, continents[0].Population);

            Assert.IsNotNull(continents[1]);
            Assert.AreEqual("test-Continent2", continents[1].Name);
            Assert.AreEqual(0, continents[1].Population);

            Assert.IsNotNull(continents[2]);
            Assert.AreEqual("test-Continent3", continents[2].Name);
            Assert.AreEqual(0, continents[2].Population);

            temp.RemoveAllContinents();
        }
        [TestMethod]
        public void Continent_Update_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));
            temp.RemoveAllContinents();
            var continent = new Continent("test-Continent");
            Assert.IsNotNull(continent);
            Assert.AreEqual("test-Continent", continent.Name);
            Assert.AreEqual(0, continent.Population);

            temp.AddContinent(continent);
            var continents = temp.GetAllContinents();

            Assert.IsNotNull(continents[0]);
            Assert.AreEqual("test-Continent", continents[0].Name);
            Assert.AreEqual(0, continents[0].Population);

            var tempContinent = temp.GetContinent(continents[0].ID);
            tempContinent.SetName("test-Continent2");

            temp.UpdateContinent(tempContinent);
            continents = temp.GetAllContinents();
            Assert.IsNotNull(continents[0]);
            Assert.AreEqual("test-Continent2", continents[0].Name);
            Assert.AreEqual(0, continents[0].Population);

            temp.RemoveAllContinents();
        }
    }
}
