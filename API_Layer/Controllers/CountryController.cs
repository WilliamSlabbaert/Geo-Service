using API_Layer.Sample_Models;
using BusinessLayer;
using BusinessLayer.Managers;
using Datalaag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [Route("api/Continent")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public ContinentManager ContinentManager { get; set; }
        public CountryManager CountryManager { get; set; }
        private readonly ILogger logger;
        public CountryController(ILogger<CountryController> logger)
        {
            this.logger = logger;
            ContinentManager = new ContinentManager(new UnitOfWork(new DataContext("test")));
            CountryManager = new CountryManager(new UnitOfWork(new DataContext("test")));
        }
        [HttpGet("{id}/Country")]
        public ActionResult<List<SampleCountry>> GetAll(int id)
        {
            try
            {
                var temp = ContinentManager.Get(id);
                logger.LogInformation("CountryController : GetAll => " + DateTime.Now);
                return CountryManager.GetByContinentName(temp).Select(s => new SampleCountry
                {
                    ID = s.ID,
                    Name = s.Name,
                    Population = s.Population,
                    Continent = $"https://localhost:5001/api/Continent/" + temp.ID,
                    Surface = s.Surface,
                    Cities = s.Cities.Select(k => $"https://localhost:5001/api/Continent/{temp.ID}/Country/{s.ID}/City/" + k.ID.ToString()).ToList(),
                    Rivers = s.Rivers.Select(x => $"https://localhost:5001/api/River/" + x.ID.ToString()).ToList()
                }).ToList();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/Country/{con_id}")]
        public ActionResult<SampleCountry> Get(int id,int con_id)
        {
            try
            {
                var tempCon = ContinentManager.Get(id);
                var temp = CountryManager.Get(con_id);
                if (tempCon.Name == temp.Continent.Name)
                {
                    logger.LogInformation("CountryController : Get => " + DateTime.Now);
                    return new SampleCountry
                    {
                        ID = temp.ID,
                        Name = temp.Name,
                        Continent = $"https://localhost:5001/api/Continent/" + temp.Continent.ID,
                        Population = temp.Population,
                        Surface = temp.Surface,
                        Cities = temp.Cities.Select(k => $"https://localhost:5001/api/Continent/{temp.Continent.ID}/Country/{temp.ID}/City/" + k.ID.ToString()).ToList(),
                        Rivers = temp.Rivers.Select(x => $"https://localhost:5001/api/River/" + x.ID.ToString()).ToList()
                    };
                }
                else
                    return NotFound("Country not found in Continent");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}/Country/{con_id}")]
        public ActionResult<SampleCountry> Put(int id, int con_id, [FromBody] SampleCountry con)
        {
            try
            {
                var tempCon = ContinentManager.Get(id);
                var temp = CountryManager.Get(con_id);

                if (con.Continent == null || con.Continent == "")
                    con.Continent = id.ToString();

                if (tempCon.Name == temp.Continent.Name)
                {
                    logger.LogInformation("CountryController : Put => " + DateTime.Now);
                    CountryManager.Update(temp, con.Name, con.Population, con.Surface, ContinentManager.Get(Int32.Parse(con.Continent)));
                    return Ok();
                }
                else
                    return NotFound("Country not found in Continent");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/Country")]
        public ActionResult<SampleCountry> Post(int id,[FromBody] SampleCountry country)
        {
            try
            {
                CountryManager = new CountryManager(new UnitOfWork(new DataContext("test")));
                var temp = new Country(country.Name, country.Population, country.Surface, ContinentManager.Get(id));
                CountryManager.Add(temp);
                logger.LogInformation("CountryController : Post => " + DateTime.Now);
                return CreatedAtAction(nameof(Get), new { id = temp.Continent.ID , con_id = temp.ID}, temp);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}/Country/{con_id}")]
        public ActionResult Delete(int id, int con_id)
        {
            try
            {
                var tempCon = ContinentManager.Get(id);
                var temp = CountryManager.Get(con_id);

                if (tempCon.Name == temp.Continent.Name)
                {
                    if(temp.Cities.Count == 0|| temp.Cities == null)
                    {
                        logger.LogInformation("CountryController : Delete => " + DateTime.Now);
                        CountryManager.Remove(con_id);
                        return NoContent();
                    }
                    else
                        return BadRequest("Country still has Cities");
                }
                else
                    return NotFound("Country not found in Continent");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
