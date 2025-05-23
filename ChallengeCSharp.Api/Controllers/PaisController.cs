using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisController : ControllerBase
    {
        private readonly PaisService _service;

        public PaisController(PaisService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> Get()
        {
            var paises = await _service.GetAllAsync();
            return Ok(paises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> Get(int id)
        {
            var pais = await _service.GetByIdAsync(id);
            if (pais == null)
                return NotFound();

            return Ok(pais);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pais pais)
        {
            await _service.AddAsync(pais);
            return CreatedAtAction(nameof(Get), new { id = pais.COD_PAIS }, pais);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pais pais)
        {
            if (id != pais.COD_PAIS)
                return BadRequest();

            await _service.UpdateAsync(pais);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}