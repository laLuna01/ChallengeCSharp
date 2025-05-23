using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TratamentoController : ControllerBase
    {
        private readonly TratamentoService _tratamentoService;

        public TratamentoController(TratamentoService tratamentoService)
        {
            _tratamentoService = tratamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tratamento>>> GetAll()
        {
            var tratamentos = await _tratamentoService.GetAllAsync();
            return Ok(tratamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tratamento>> GetById(int id)
        {
            var tratamento = await _tratamentoService.GetByIdAsync(id);
            if (tratamento == null)
                return NotFound();
            return Ok(tratamento);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Tratamento tratamento)
        {
            await _tratamentoService.AddAsync(tratamento);
            return CreatedAtAction(nameof(GetById), new { id = tratamento.ID_TRATAMENTO }, tratamento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Tratamento tratamento)
        {
            if (id != tratamento.ID_TRATAMENTO)
                return BadRequest("ID do tratamento não corresponde ao informado na URL.");

            await _tratamentoService.UpdateAsync(tratamento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tratamento = await _tratamentoService.GetByIdAsync(id);
            if (tratamento == null)
                return NotFound();

            await _tratamentoService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("consultas")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultas()
        {
            var consultas = await _tratamentoService.GetAllConsultasAsync();
            return Ok(consultas);
        }
    }
}
