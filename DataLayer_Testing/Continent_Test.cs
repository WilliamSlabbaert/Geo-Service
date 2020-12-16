using Datalaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer_Testing
{
    [TestClass]
    public class Continent_Test
    {
        [TestMethod]
        public void Test_Repo_Continents()
        {
            var temp = new UnitOfWork(new DataContext("test"));
            temp.continentRepo.removeAll();
            temp.continentRepo.add(new BusinessLayer.Continent("test-Continent"));
            temp.Complete();
            var continents = temp.continentRepo.getAll();
            var continent = temp.continentRepo.getById(continents[0].ID);

            Assert.AreEqual("test-Continent", continent.Name);
            Assert.AreEqual(0, continent.Population);
            temp.continentRepo.removeAll();
            temp.Complete();

            temp.continentRepo.add(new BusinessLayer.Continent("test-Continent1"));
            temp.Complete();
            temp.continentRepo.add(new BusinessLayer.Continent("test-Continent2"));
            temp.Complete();
            temp.continentRepo.add(new BusinessLayer.Continent("test-Continent3"));
            temp.Complete();
            continents = temp.continentRepo.getAll();

            Assert.AreEqual("test-Continent1", continents[0].Name);
            Assert.AreEqual(0, continents[0].Population);

            Assert.AreEqual("test-Continent2", continents[1].Name);
            Assert.AreEqual(0, continents[1].Population);

            Assert.AreEqual("test-Continent3", continents[2].Name);
            Assert.AreEqual(0, continents[2].Population);

            Assert.AreEqual(3, continents.Count);
            temp.continentRepo.delete(continents[1].ID);
            temp.Complete();
            continents = temp.continentRepo.getAll();
            Assert.AreEqual(2,continents.Count);

            continent = temp.continentRepo.getById(continents[0].ID);

            Assert.AreEqual("test-Continent1", continent.Name);
            Assert.AreEqual(0, continent.Population);
            
            continent.SetName("test");
            temp.continentRepo.update(continent);
            temp.Complete();

            continents = temp.continentRepo.getAll();
            continent = temp.continentRepo.getById(continents[0].ID);

            Assert.AreEqual("test", continent.Name);
            Assert.AreEqual(0, continent.Population);
            temp.continentRepo.removeAll();
            temp.Complete();
        }
    }
}
