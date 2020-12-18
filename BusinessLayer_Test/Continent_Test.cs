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

        }
        [TestMethod]
        public void Continent_Get_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));

        }
        [TestMethod]
        public void Continent_Delete_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));

        }
        [TestMethod]
        public void Continent_GetAll_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));

        }
        [TestMethod]
        public void Continent_Update_Test()
        {
            ContinentManager temp = new ContinentManager(new UnitOfWork(new DataContext("test")));

        }
    }
}
