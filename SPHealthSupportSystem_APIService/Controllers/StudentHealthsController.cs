using Microsoft.AspNetCore.Mvc;
using SPHealthSupportSystem_Repositories.Models;
using SPHealthSupportSystem_Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPHealthSupportSystem_APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsychologyTheorysController : ControllerBase
    {
        private readonly IPsychologyTheoryService _psychologyTheoryService;
        public PsychologyTheorysController(IPsychologyTheoryService psychologyTheoryService)
        {
            _psychologyTheoryService = psychologyTheoryService;
        }
        // GET: api/<PsychologyTheorysController>
        [HttpGet]
        public async Task<IEnumerable<PsychologyTheory>> Get()
        {
            return await _psychologyTheoryService.GetAll();
        }
        [HttpGet("{name}/{topicName}/{author}")]

        public async Task<IEnumerable<PsychologyTheory>> Search(string? name, string? topicName, string? author)
        {
            return await _psychologyTheoryService.Search(name, topicName, author);
        }

        // GET api/<PsychologyTheorysController>/5
        [HttpGet("{id}")]
        public PsychologyTheory GetById(int id)
        {
            return _psychologyTheoryService.GetById(id);
        }

        // POST api/<PsychologyTheorysController>
        [HttpPost]
        public async Task<int> Post([FromBody] PsychologyTheory psychologyTheory)
        {
            return await _psychologyTheoryService.Create(psychologyTheory);
        }

        // PUT api/<PsychologyTheorysController>/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] PsychologyTheory psychologyTheory)
        {
            return await _psychologyTheoryService.Update(psychologyTheory);
        }

        // DELETE api/<PsychologyTheorysController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _psychologyTheoryService.Delete(id);
        }
    }
}
