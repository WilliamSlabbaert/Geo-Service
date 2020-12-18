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
            temp.continentRepo.add(new BusinessLayer.Continent("test-continent"));
            temp.continentRepo.add(new BusinessLayer.Continent("test-continent1"));

            var continents = temp.continentRepo.getAll();
            var continent1 = temp.continentRepo.getById(continents[0].ID);
            var continent2 = temp.continentRepo.getById(continents[1].ID);
            Assert.AreEqual(2, continents.Count);
            Assert.AreEqual("test-continent",continent1.Name);
            Assert.AreEqual("test-continent1", continent2.Name);

            continent1.SetName("test-rename");
            temp.continentRepo.update(continent1);

            continents = temp.continentRepo.getAll();
            continent1 = temp.continentRepo.getById(continents[0].ID);
            continent2 = temp.continentRepo.getById(continents[1].ID);

            Assert.AreEqual(2, continents.Count);
            Assert.AreEqual("test-rename", continent1.Name);
            Assert.AreEqual("test-continent1", continent2.Name);

            temp.continentRepo.delete(continent1.ID);
            continents = temp.continentRepo.getAll();
            Assert.AreEqual(1, continents.Count);

            temp.continentRepo.removeAll();
        }
    }
}
