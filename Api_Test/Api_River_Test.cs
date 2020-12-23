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
    public class Api_River_Test
    {
        private readonly RiverController riverController;
        private readonly Mock<ILogger<RiverController>> _mockLogger;


        public Api_River_Test()
        {
            this._mockLogger = new Mock<ILogger<RiverController>>();
            this.riverController = new RiverController(_mockLogger.Object);
        }
        [Fact]
        public void Add_RiverController_Test()
        {
            riverController.Post(new API_Layer.Sample_Models.SampleRiver { Name= "river_test",Lenght = 250,Countries=new List<String> { "608" } });
            var temp = riverController.RiverManager.GetAll().Last();
            Assert.Equal("river_test", temp.Name);
            Assert.Equal(250, temp.Lenght);

            riverController.Delete(temp.ID);
        }
        [Fact]
        public void Put_RiverController_Test()
        {
            riverController.Post(new API_Layer.Sample_Models.SampleRiver { Name = "river_test", Lenght = 250, Countries = new List<String> { "608" } });
            var temp = riverController.RiverManager.GetAll().Last();
            Assert.Equal("river_test", temp.Name);
            Assert.Equal(250, temp.Lenght);

            riverController.Put(temp.ID,new API_Layer.Sample_Models.SampleRiver { Name = "river_test1", Lenght = 50, Countries = new List<String> { "608" } });
            temp = riverController.RiverManager.GetAll().Last();
            Assert.Equal("river_test1", temp.Name);
            Assert.Equal(50, temp.Lenght);

            riverController.Delete(temp.ID);
        }
    }
}
