using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonApi.DTOs;
using PersonApi.Models;
using PersonApi.Repositories;

namespace PersonApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonController> _logger;
        
        public PersonController(IPersonRepository personRepository, IMapper mapper, ILogger<PersonController> logger)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _logger = logger;
        }
        
        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
        {
            try
            {
                var persons = await _personRepository.GetAllPersonsAsync();
                var personDtos = _mapper.Map<IEnumerable<PersonDto>>(persons);
                return Ok(personDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all persons");
                return StatusCode(500, "Internal server error");
            }
        }
        
        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(int id)
        {
            try
            {
                var person = await _personRepository.GetPersonByIdAsync(id);
                
                if (person == null)
                    return NotFound($"Person with ID {id} not found");
                
                var personDto = _mapper.Map<PersonDto>(person);
                return Ok(personDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting person with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        // POST: api/Person
        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePerson(CreatePersonDto createPersonDto)
        {
            try
            {
                var person = _mapper.Map<Person>(createPersonDto);
                var createdPerson = await _personRepository.CreatePersonAsync(person);
                var personDto = _mapper.Map<PersonDto>(createdPerson);
                
                return CreatedAtAction(nameof(GetPerson), new { id = personDto.Id }, personDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a person");
                return StatusCode(500, "Internal server error");
            }
        }
        
        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, UpdatePersonDto updatePersonDto)
        {
            try
            {
                var personToUpdate = _mapper.Map<Person>(updatePersonDto);
                var updatedPerson = await _personRepository.UpdatePersonAsync(id, personToUpdate);
                
                if (updatedPerson == null)
                    return NotFound($"Person with ID {id} not found");
                
                var personDto = _mapper.Map<PersonDto>(updatedPerson);
                return Ok(personDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating person with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var result = await _personRepository.DeletePersonAsync(id);
                
                if (!result)
                    return NotFound($"Person with ID {id} not found");
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting person with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}