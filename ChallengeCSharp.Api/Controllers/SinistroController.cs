using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinistroController : ControllerBase
    {
        private readonly SinistroService _sinistroService;

        public SinistroController(SinistroService sinistroService)
        {
            _sinistroService = sinistroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sinistro>>> GetAll()
        {
            var sinistros = await _sinistroService.GetAllAsync();
            return Ok(sinistros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sinistro>> GetById(int id)
        {
            var sinistro = await _sinistroService.GetByIdAsync(id);
            if (sinistro == null)
                return NotFound();
            return Ok(sinistro);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Sinistro sinistro)
        {
            await _sinistroService.AddAsync(sinistro);
            return CreatedAtAction(nameof(GetById), new { id = sinistro.ID_SINISTRO }, sinistro);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Sinistro sinistro)
        {
            if (id != sinistro.ID_SINISTRO)
                return BadRequest("ID do sinistro não corresponde ao informado na URL.");

            await _sinistroService.UpdateAsync(sinistro);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var sinistro = await _sinistroService.GetByIdAsync(id);
            if (sinistro == null)
                return NotFound();

            await _sinistroService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("consultas")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultas()
        {
            var consultas = await _sinistroService.GetAllConsultasAsync();
            return Ok(consultas);
        }
    }
}
