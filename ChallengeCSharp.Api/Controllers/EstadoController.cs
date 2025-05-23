using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : ControllerBase
    {
        private readonly EstadoService _service;

        public EstadoController(EstadoService service)
        {
            _service = service;
        }

        // GET: api/estado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> Get()
        {
            var estados = await _service.GetAllAsync();
            return Ok(estados);
        }

        // GET: api/estado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> Get(int id)
        {
            var estado = await _service.GetByIdAsync(id);
            if (estado == null)
                return NotFound();

            return Ok(estado);
        }

        // POST: api/estado
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Estado estado)
        {
            await _service.AddAsync(estado);
            return CreatedAtAction(nameof(Get), new { id = estado.COD_ESTADO }, estado);
        }

        // PUT: api/estado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Estado estado)
        {
            if (id != estado.COD_ESTADO)
                return BadRequest();

            await _service.UpdateAsync(estado);
            return NoContent();
        }

        // DELETE: api/estado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/estado/paises
        [HttpGet("paises")]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises()
        {
            var paises = await _service.GetAllPaisesAsync();
            return Ok(paises);
        }
    }
}