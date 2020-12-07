using BusinessLayer;
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
            Continent con = new Continent("Test_Name", 500);
            temp.AddContinent(con);
        }
    }
}
