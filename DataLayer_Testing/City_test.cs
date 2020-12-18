using Datalaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer_Testing
{
    [TestClass]
    public class City_test
    {
        [TestMethod]
        public void Test_Repo_City()
        {
            var tempManager = new UnitOfWork(new DataContext("test"));

        }
    }
}
