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
    [Route("api/River")]
    public class RiverController : ControllerBase
    {
        public ContinentManager ContinentManager { get; set; }
        public CountryManager CountryManager { get; set; }
        public RiverManager RiverManager { get; set; }
        private readonly ILogger logger;
        public RiverController(ILogger<RiverController> logger)
        {
            this.logger = logger;
            ContinentManager = new ContinentManager(new UnitOfWork(new DataContext("test")));
            CountryManager = new CountryManager(new UnitOfWork(new DataContext("test")));
            RiverManager = new RiverManager(new UnitOfWork(new DataContext("test")));
        }
        [HttpGet("{id}")]
        public ActionResult<SampleRiver> Get(int id)
        {
            
            try
            {
                var temp = RiverManager.Get(id);
                if (temp == null)
                    return NotFound("River is not found");
                logger.LogInformation("RiverController : Get => " + DateTime.Now);
               
                var MainList = new List<Country>();

                List<Country> tempCountries = CountryManager.GetAll().FindAll(s=>s.Rivers.Count != 0).ToList();

                foreach(var country in tempCountries)
                    foreach(var river in country.Rivers)
                        if(river.ID == id)
                            MainList.Add(country);
                    
                
                return new SampleRiver { ID = temp.ID, 
                    Name = temp.Name, 
                    Lenght = temp.Lenght, 
                    Countries = MainList.Select(s => $"https://localhost:5001/api/Continent/{s.Continent.ID}/Country/" + s.ID).ToList() };
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<SampleRiver> Delete(int id)
        {
            try
            {
                var temp = RiverManager.Get(id);
                if (temp == null)
                    return NotFound("River is not found");
                else
                {
                    logger.LogInformation("RiverController : Delete => " + DateTime.Now);
                    RiverManager.Remove(id);
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult<SampleRiver> Put(int id, [FromBody] SampleRiver river)
        {
            try
            {
                var temp = RiverManager.Get(id);
                if (temp == null)
                    return NotFound("River is not found");
                else
                {
                    List<Country> tempList = new List<Country>();
                    foreach (var country in CountryManager.GetAll())
                    {
                        if (river.Countries.Contains(country.ID.ToString()))
                            tempList.Add(country);
                    }
                    if (tempList.Count == river.Countries.Count)
                    {
                        logger.LogInformation("RiverController : Put => " + DateTime.Now);
                        
                        RiverManager.Update(temp,river.Name,river.Lenght, tempList);
                        return Ok();
                    }
                    else
                        return NotFound("Country input not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] SampleRiver river)
        {
            try
            {
                List<Country> tempList = new List<Country>();
                foreach (var country in CountryManager.GetAll())
                {
                    if (river.Countries.Contains(country.ID.ToString()))
                        tempList.Add(country);
                }
                if (tempList.Count == river.Countries.Count)
                {
                    logger.LogInformation("RiverController : Get => " + DateTime.Now);
                    River x = new River(river.Name, river.Lenght, tempList);
                    RiverManager.Add(x);
                    return CreatedAtAction(nameof(Get), new { id = x.ID }, x);
                }
                else
                    return NotFound("Country input not found");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
