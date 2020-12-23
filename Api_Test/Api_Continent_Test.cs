using API_Layer.Controllers;
using API_Layer.Sample_Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Api_Test
{
    public class Api_Continent_Test
    {
        private readonly ContinentController continentController;
        private readonly Mock<ILogger<ContinentController>> _mockLogger;


        public Api_Continent_Test()
        {
            this._mockLogger = new Mock<ILogger<ContinentController>>();
            this.continentController = new ContinentController(_mockLogger.Object);
        }
        [Fact]
        public void Get_Test_Continent()
        {
            var temp = continentController.Get(584).Value as SampleContinent;

            Assert.Equal("test-Continent", temp.Name);
            Assert.Equal(50, temp.Population);
            Assert.Single(temp.Countries);
        }
        [Fact]
        public void Add_Test_Continent()
        {
            continentController.AddContinent(new SampleContinent { Name = "testing" });
            var temp = (continentController.GetAll().Value as List<SampleContinent>).Last();

            Assert.Equal("testing", temp.Name);
            Assert.Equal(0, temp.Population);
            Assert.Empty(temp.Countries);

            continentController.Delete(Int32.Parse(temp.ID.Replace("https://localhost:5001/api/Continent/","")));
        }
        [Fact]
        public void Put_Test_Continent()
        {
            continentController.AddContinent(new SampleContinent { Name = "testing" });
            var temp = (continentController.GetAll().Value as List<SampleContinent>).Last();
            continentController.Put(Int32.Parse(temp.ID.Replace("https://localhost:5001/api/Continent/", "")), new SampleContinent { Name = "test" });
            temp = (continentController.GetAll().Value as List<SampleContinent>).Last();

            Assert.Equal("test", temp.Name);
            Assert.Equal(0, temp.Population);
            Assert.Empty(temp.Countries);

            continentController.Delete(Int32.Parse(temp.ID.Replace("https://localhost:5001/api/Continent/", "")));
        }
    }
}
