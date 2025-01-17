using Microsoft.AspNetCore.Mvc;
using SPHealthSupportSystem_Repositories.Models;
using SPHealthSupportSystem_Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPHealthSupportSystem_APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentHealthsController : ControllerBase
    {
        private readonly IStudentHealthService _studentHealthService;
        public StudentHealthsController(IStudentHealthService studentHealthService)
        {
            _studentHealthService = studentHealthService;
        }
        // GET: api/<StudentHealthsController>
        [HttpGet]
        public async Task<IEnumerable<StudentHealth>> Get()
        {
            return await _studentHealthService.GetAll();
        }

        // GET api/<StudentHealthsController>/5
        [HttpGet("{id}")]
        public StudentHealth GetById(int id)
        {
            return _studentHealthService.GetById(id);
        }

        // POST api/<StudentHealthsController>
        [HttpPost]
        public async Task<int> Post([FromBody] StudentHealth studentHealth)
        {
            return await _studentHealthService.Create(studentHealth);
        }

        // PUT api/<StudentHealthsController>/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] StudentHealth studentHealth)
        {
            return await _studentHealthService.Update(studentHealth);
        }

        // DELETE api/<StudentHealthsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _studentHealthService.Delete(id);
        }
    }
}
