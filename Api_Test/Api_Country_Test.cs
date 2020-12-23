using API_Layer.Controllers;
using API_Layer.Sample_Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Api_Test
{
    public class Api_Country_Test
    {
        private readonly CountryController countryController;
        private readonly Mock<ILogger<CountryController>> _mockLogger;

        public Api_Country_Test()
        {
            this._mockLogger = new Mock<ILogger<CountryController>>();
            this.countryController = new CountryController(_mockLogger.Object);
        }
        [Fact]
        public void Get_Test_Country()
        {
            var temp = countryController.Get(584,608).Value as SampleCountry;

            Assert.Equal("test-Country1", temp.Name);
            Assert.Equal(50, temp.Population);
            Assert.Equal(20, temp.Surface);
            Assert.Empty(temp.Cities);
        }
        [Fact]
        public void Add_Test_Country()
        {
            countryController.Post(585,new SampleCountry { Name = "city_test",Population = 150,Surface = 500});
            var temp = countryController.CountryManager.GetAll().Last();

            Assert.Equal("city_test", temp.Name);
            Assert.Equal(150, temp.Population);
            Assert.Equal(500, temp.Surface);

            countryController.Delete(585,temp.ID);
        }
        [Fact]
        public void Put_Test_Country()
        {
            countryController.Post(585, new SampleCountry { Name = "city_test", Population = 150, Surface = 500 });
            var temp = countryController.CountryManager.GetAll().Last();
            Assert.Equal("city_test", temp.Name);
            Assert.Equal(150, temp.Population);
            Assert.Equal(500, temp.Surface);

            countryController.Put(585, temp.ID, new SampleCountry { Name = "test", Population = 50, Surface = 50 });
            temp = countryController.CountryManager.GetAll().Last();

            Assert.Equal("test", temp.Name);
            Assert.Equal(50, temp.Population);
            Assert.Equal(50, temp.Surface);

            countryController.Delete(585, temp.ID);
        }
    }
}
