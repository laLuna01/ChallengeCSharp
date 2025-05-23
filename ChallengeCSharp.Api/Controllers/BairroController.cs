using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BairroController : ControllerBase
    {
        private readonly BairroService _service;

        public BairroController(BairroService service)
        {
            _service = service;
        }

        // GET: api/bairro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bairro>>> Get()
        {
            var bairros = await _service.GetAllAsync();
            return Ok(bairros);
        }

        // GET: api/bairro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bairro>> Get(int id)
        {
            var bairro = await _service.GetByIdAsync(id);
            if (bairro == null)
                return NotFound();

            return Ok(bairro);
        }

        // POST: api/bairro
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Bairro bairro)
        {
            await _service.AddAsync(bairro);
            return CreatedAtAction(nameof(Get), new { id = bairro.COD_BAIRRO }, bairro);
        }

        // PUT: api/bairro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Bairro bairro)
        {
            if (id != bairro.COD_BAIRRO)
                return BadRequest();

            await _service.UpdateAsync(bairro);
            return NoContent();
        }

        // DELETE: api/bairro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/bairro/cidades
        [HttpGet("cidades")]
        public async Task<ActionResult<IEnumerable<Cidade>>> GetCidades()
        {
            var cidades = await _service.GetAllCidadesAsync();
            return Ok(cidades);
        }
    }
}
