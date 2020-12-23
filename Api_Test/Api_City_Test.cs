using API_Layer.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Api_Test
{
    public class Api_City_Test
    {
        private readonly CityController cityController;
        private readonly Mock<ILogger<CityController>> _mockLogger;


        public Api_City_Test()
        {
            this._mockLogger = new Mock<ILogger<CityController>>();
            this.cityController = new CityController(_mockLogger.Object);
        }
        [Fact]
        public void Add_CityController_Test()
        {
            cityController.Post(584, 608, new API_Layer.Sample_Models.SampleCity { name = "test-city", Population = 50,Capital = true });
            var temp = cityController.CityManager.GetAll().Last();

            Assert.Equal("test-city", temp.Name);
            Assert.Equal(50, temp.Population);
            Assert.True(temp.IsCapital);

            cityController.Delete(584,608,temp.ID);
        }
        [Fact]
        public void Put_CityController_Test()
        {
            cityController.Post(584, 608, new API_Layer.Sample_Models.SampleCity { name = "test-city", Population = 50, Capital = true });
            var temp = cityController.CityManager.GetAll().Last();
            

            Assert.Equal("test-city", temp.Name);
            Assert.Equal(50, temp.Population);
            Assert.True(temp.IsCapital);

            cityController.Put(584, 608, temp.ID, new API_Layer.Sample_Models.SampleCity { name = "test", Population = 20, Capital = false, Country = "608" });
            temp = cityController.CityManager.GetAll().Last();
            Assert.Equal("test", temp.Name);
            Assert.Equal(20, temp.Population);
            Assert.False(temp.IsCapital);

            cityController.Delete(584, 608, temp.ID);
        }
    }
}
