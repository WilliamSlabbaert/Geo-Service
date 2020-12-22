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
    public class ContinentController : ControllerBase
    {
        public ContinentManager ContinentManager { get; set; }
        public CountryManager CountryManager { get; set; }
        private readonly ILogger logger;
        public ContinentController(ILogger<ContinentController> logger)
        {
            this.logger = logger;
            ContinentManager = new ContinentManager(new UnitOfWork(new DataContext("test")));
            CountryManager = new CountryManager(new UnitOfWork(new DataContext("test")));
        }
        [HttpGet]
        public ActionResult<List<SampleContinent>> GetAll()
        {
            try
            {
                logger.LogInformation("ContinentController : GetAll => " + DateTime.Now);
                return ContinentManager.GetAll().Select(s => new SampleContinent
                {
                    ID = $"https://localhost:5001/api/Continent/{s.ID}",
                    Name = s.Name,
                    Population = s.Population,
                    Countries = CountryManager.GetByContinentName(s).Select(k => $"https://localhost:5001/api/Continent/{s.ID}/Country/" + k.ID.ToString()).ToList()
                }).ToList();
            }
            catch
            {
                Response.StatusCode = 400;
                return null;
            }
        }
        [HttpPost]
        public ActionResult<SampleContinent> AddContinent([FromBody] Sample_Models.SampleContinent con)
        {
            try
            {
                Continent temp = new Continent(con.Name);
                if (ContinentManager.IfExist(temp))
                {
                    ContinentManager.Add(temp);
                    logger.LogInformation("ContinentController : Add => " + DateTime.Now);
                    return CreatedAtAction(nameof(Get), new { id = temp.ID }, temp);
                }
                else
                    return BadRequest("Continent already exist");
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong : " + e);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<SampleContinent> Get(int id)
        {
            try
            {
                var temp = ContinentManager.Get(id);
                var tempList = CountryManager.GetByContinentName(temp).Select(s => $"https://localhost:5001/api/Continent/{temp.ID}/Country/" + s.ID.ToString()).ToList();
                logger.LogInformation("ContinentController : Get => " + DateTime.Now);
                return new SampleContinent { ID = temp.ID.ToString(), Name = temp.Name, Population = temp.Population, Countries = tempList };
            }
            catch (Exception e)
            {
                return NotFound("Continent doesn't exist");
            }
        }
        [HttpPut("{id}")]
        public ActionResult<SampleContinent> Put(int id, [FromBody] SampleContinent con)
        {
            try
            {
                var temp = ContinentManager.Get(id);

                if (ContinentManager.IfExist(new Continent(con.Name)))
                {
                    logger.LogInformation("ContinentController : Put => " + DateTime.Now);
                    temp.SetName(con.Name);
                    ContinentManager.Update(temp);
                    return CreatedAtAction(nameof(Get), new { id = temp.ID }, temp);
                }
                else
                    return BadRequest("Continent already Exists");
            }
            catch (Exception e)
            {
                return NotFound("Continent doesn't exist");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var temp = ContinentManager.Get(id);
                if (temp != null)
                {
                    if (CountryManager.GetByContinentName(temp).Count == 0)
                    {
                        logger.LogInformation("ContinentController : Delete => " + DateTime.Now);
                        ContinentManager.Remove(id);
                        return NoContent();
                    }
                    else
                        return BadRequest("Continent still has Countries");
                }
                else
                    return NotFound("Continent not found");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
