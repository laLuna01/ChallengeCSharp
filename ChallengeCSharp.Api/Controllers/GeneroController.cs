using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneroController : ControllerBase
    {
        private readonly GeneroService _service;

        public GeneroController(GeneroService service)
        {
            _service = service;
        }

        // GET: api/genero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            var generos = await _service.GetAllAsync();
            return Ok(generos);
        }

        // GET: api/genero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await _service.GetByIdAsync(id);
            if (genero == null)
                return NotFound();

            return Ok(genero);
        }

        // POST: api/genero
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Genero genero)
        {
            await _service.AddAsync(genero);
            return CreatedAtAction(nameof(Get), new { id = genero.ID_GENERO }, genero);
        }

        // PUT: api/genero/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Genero genero)
        {
            if (id != genero.ID_GENERO)
                return BadRequest();

            await _service.UpdateAsync(genero);
            return NoContent();
        }

        // DELETE: api/genero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}