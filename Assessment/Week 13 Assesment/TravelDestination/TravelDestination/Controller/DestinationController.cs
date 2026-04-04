using Microsoft.AspNetCore.Mvc;

namespace TravelDestination.Backend.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using TravelDestination.Backend.Models;
    using TravelDestination.Backend.Repository;

    namespace YourProjectName.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class DestinationsController : ControllerBase
        {
            private readonly IDestinationRepository _repository;

            public DestinationsController(IDestinationRepository repository)
            {
                _repository = repository;
            }

           
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var destinations = await _repository.GetAllAsync();
                return Ok(destinations);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var destination = await _repository.GetByIdAsync(id);

                if (destination == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = $"Destination with ID {id} not found"
                    });
                }

                return Ok(destination);
            }

            [HttpPost]
            public async Task<IActionResult> Create(Destination destination)
            {
                // Ensure the DB generates the key; ignore any client-supplied ID
                destination.DestinationID = 0;

                await _repository.AddAsync(destination);

                return CreatedAtAction(nameof(GetById),
                    new { id = destination.DestinationID },
                    destination);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, Destination destination)
            {
                if (id != destination.DestinationID)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "ID mismatch"
                    });
                }

                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = $"Destination with ID {id} not found"
                    });
                }

                await _repository.UpdateAsync(destination);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var existing = await _repository.GetByIdAsync(id);

                if (existing == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = $"Destination with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(id);
                return NoContent();
            }
        }
    }
}
