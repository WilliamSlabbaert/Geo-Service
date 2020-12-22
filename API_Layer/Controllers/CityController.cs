using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Layer.Sample_Models;
using BusinessLayer;
using BusinessLayer.Managers;
using Datalaag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("api/Continent")]
    public class CityController : ControllerBase
    {
        public ContinentManager ContinentManager { get; set; }
        public CountryManager CountryManager { get; set; }
        public CityManager CityManager { get; set; }
        private readonly ILogger logger;
        public CityController(ILogger<ContinentController> logger)
        {
            this.logger = logger;
            ContinentManager = new ContinentManager(new UnitOfWork(new DataContext("test")));
            CountryManager = new CountryManager(new UnitOfWork(new DataContext("test")));
            CityManager = new CityManager(new UnitOfWork(new DataContext("test")));
        }
        [HttpGet("{id}/Country/{country_id}/City/{city_id}")]
        public ActionResult<SampleCity> Get(int id, int country_id,int city_id)
        {
            try
            {
                var tempCon = ContinentManager.Get(id);
                var tempCou = CountryManager.Get(country_id);
                if (tempCon.Name == tempCou.Continent.Name)
                {
                    var temp = CityManager.Get(city_id);
                    if(temp == null)
                        return NotFound("City not found in Country");
                    else
                    {
                        logger.LogInformation("CityController : Get => " + DateTime.Now);
                        return new SampleCity { ID = temp.ID, name = temp.Name, Capital = temp.IsCapital, Country = $"https://localhost:5001/api/Continent/{id}/Country/{country_id}", Population = temp.Population };
                    }
                }
                else
                    return NotFound("Country not found in Continent");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}/Country/{country_id}/City/{city_id}")]
        public ActionResult<SampleCity> Delete(int id, int country_id, int city_id)
        {
            try
            {
                var tempCon = ContinentManager.Get(id);
                var tempCou = CountryManager.Get(country_id);
                if (tempCon.Name == tempCou.Continent.Name)
                {
                    var temp = CityManager.Get(city_id);
                    if (temp == null)
                        return NotFound("City not found in Country");
                    else
                    {
                        logger.LogInformation("CityController : Delete => " + DateTime.Now);
                        CityManager.Remove(city_id);
                        return NoContent();
                    }
                }
                else
                    return NotFound("Country not found in Continent");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}/Country/{country_id}/City/{city_id}")]
        public ActionResult<SampleCity> Put(int id, int country_id, int city_id, [FromBody] SampleCity con)
        {
            try
            {
                var tempCon = ContinentManager.Get(id);
                var tempCou = CountryManager.Get(country_id);
                var getNewCountry = CountryManager.Get(Int32.Parse(con.Country));
                if (tempCon.Name == tempCou.Continent.Name)
                {
                    var temp = CityManager.Get(city_id);
                    if (temp == null)
                        return NotFound("City not found in Country");
                    else
                    {
                        
                        if (getNewCountry != null)
                        {
                            CityManager.Update(temp, con.name, con.Population, con.Capital, getNewCountry);
                            logger.LogInformation("CityController : Put => " + DateTime.Now);
                            return Ok();
                        }
                        else
                            return NotFound("Country input not found");
                    }
                }
                else
                    return NotFound("Country not found in Continent");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/Country/{country_id}/City")]
        public ActionResult Post(int id,int country_id,[FromBody] SampleCity con)
        {
            try
            {
                var tempCon = ContinentManager.Get(id);
                var tempCou = CountryManager.Get(country_id);
                if (tempCon.Name == tempCou.Continent.Name)
                {
                    var temp = new City(con.name, con.Population, tempCou, con.Capital);
                    if (CityManager.CalculatePopulationCheck(temp))
                    {
                        CityManager.Add(temp);
                        logger.LogInformation("CityController : Post => " + DateTime.Now);
                        return CreatedAtAction(nameof(Get), new { id = tempCon.ID, country_id = tempCou.ID, city_id = temp.ID }, temp);
                    }
                    else
                        return BadRequest("Too much population for Country");
                }
                else
                    return NotFound("Country not found in Continent");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
