using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteService _service;

        public PacienteController(PacienteService service)
        {
            _service = service;
        }

        // GET: api/paciente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> Get()
        {
            var pacientes = await _service.GetAllAsync();
            return Ok(pacientes);
        }

        // GET: api/paciente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> Get(int id)
        {
            var paciente = await _service.GetByIdAsync(id);
            if (paciente == null)
                return NotFound();

            return Ok(paciente);
        }

        // POST: api/paciente
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Paciente paciente)
        {
            await _service.AddAsync(paciente);
            return CreatedAtAction(nameof(Get), new { id = paciente.ID_PACIENTE }, paciente);
        }

        // PUT: api/paciente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Paciente paciente)
        {
            if (id != paciente.ID_PACIENTE)
                return BadRequest();

            await _service.UpdateAsync(paciente);
            return NoContent();
        }

        // DELETE: api/paciente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/paciente/generos
        [HttpGet("generos")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            var generos = await _service.GetAllGenerosAsync();
            return Ok(generos);
        }

        // GET: api/paciente/enderecos
        [HttpGet("enderecos")]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos()
        {
            var enderecos = await _service.GetAllEnderecosAsync();
            return Ok(enderecos);
        }
    }
}
