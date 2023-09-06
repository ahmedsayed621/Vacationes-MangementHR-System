using Microsoft.AspNetCore.Mvc;
using Vacationes_MangementHR_System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vacationes_MangementHR_System.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationPlanApiController : ControllerBase
    {
        private readonly VacationDbContext _context;

        public VacationPlanApiController(VacationDbContext context)
        {
            _context = context;
        }
        // GET: api/<VacationPlanApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VacationPlanApiController>/5
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                return Ok(_context.Employees.Where(x=>x.Name.Contains(name)).ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<VacationPlanApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VacationPlanApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VacationPlanApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
