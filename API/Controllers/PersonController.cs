using System.Collections.Generic;
using System.Threading.Tasks;
using API.Helper;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger,
            IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet("Search/{condition}")]
        public async Task<IEnumerable<PersonDto>> GetAll(string condition)
        {
            return await _personService.GetAllByCondition(condition);
        }

        [HttpGet("{id}")]
        public async Task<PersonDto> GetById(int id)
        {

            return await _personService.GetById(id);

        }

        [HttpPost]
        public async Task<PersonDto> Create([FromBody] PersonPostDto person)
        {

            return await _personService.Save(person);

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonPutDto person)
        {

            return Ok(await _personService.Update(person));

        }

        [HttpDelete("{id}/connection")]
        public async Task<PersonDto> Delete(int id,ConnectionDto connectionDto)
        {
            return await _personService.DeleteConnection(id, connectionDto);
        }

        [HttpPost("{id}/connection")]
        public async Task<ConnectionDto> Addconnection(int id, ConnectionDto connectionDto)
        {
            return await _personService.AddConnection(id, connectionDto);
        }

        [HttpDelete("{id}")]
        public async Task<PersonDto> Delete(int id)
        {
            return await _personService.Delete(id);
        }

        [HttpGet("{id}/connections/{condition}")]
        public async Task<IEnumerable<ConnectionDto>> GetConnectionsByType(int id, string condition)
        {
            return await _personService.GetConnectionsByType(id, condition);
        }

        [HttpPost("{personId}/image")]
        public async Task<PersonDto> UploadImage(IFormFile FilePath, int personId)
        {

            var ImageUrl = await ImageHelper.Upload(FilePath);
            return await _personService.UpdateImage(personId, ImageUrl);



        }

    }
}
