using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DentistaController : ControllerBase
    {
        private readonly DentistaService _service;

        public DentistaController(DentistaService service)
        {
            _service = service;
        }

        // GET: api/dentista
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dentista>>> Get()
        {
            var dentistas = await _service.GetAllAsync();
            return Ok(dentistas);
        }

        // GET: api/dentista/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dentista>> Get(int id)
        {
            var dentista = await _service.GetByIdAsync(id);
            if (dentista == null)
                return NotFound();

            return Ok(dentista);
        }

        // POST: api/dentista
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dentista dentista)
        {
            await _service.AddAsync(dentista);
            return CreatedAtAction(nameof(Get), new { id = dentista.ID_DENTISTA }, dentista);
        }

        // PUT: api/dentista/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Dentista dentista)
        {
            if (id != dentista.ID_DENTISTA)
                return BadRequest();

            await _service.UpdateAsync(dentista);
            return NoContent();
        }

        // DELETE: api/dentista/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/dentista/generos
        [HttpGet("generos")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            var generos = await _service.GetAllGenerosAsync();
            return Ok(generos);
        }

        // GET: api/dentista/enderecos
        [HttpGet("enderecos")]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos()
        {
            var enderecos = await _service.GetAllEnderecosAsync();
            return Ok(enderecos);
        }
    }
}
