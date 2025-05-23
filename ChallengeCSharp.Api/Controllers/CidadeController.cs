using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _service;

        public CidadeController(CidadeService service)
        {
            _service = service;
        }

        // GET: api/cidade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cidade>>> Get()
        {
            var cidades = await _service.GetAllAsync();
            return Ok(cidades);
        }

        // GET: api/cidade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cidade>> Get(int id)
        {
            var cidade = await _service.GetByIdAsync(id);
            if (cidade == null)
                return NotFound();

            return Ok(cidade);
        }

        // POST: api/cidade
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cidade cidade)
        {
            await _service.AddAsync(cidade);
            return CreatedAtAction(nameof(Get), new { id = cidade.COD_CIDADE }, cidade);
        }

        // PUT: api/cidade/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cidade cidade)
        {
            if (id != cidade.COD_CIDADE)
                return BadRequest();

            await _service.UpdateAsync(cidade);
            return NoContent();
        }

        // DELETE: api/cidade/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/cidade/estados
        [HttpGet("estados")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            var estados = await _service.GetAllEstadosAsync();
            return Ok(estados);
        }
    }
}
