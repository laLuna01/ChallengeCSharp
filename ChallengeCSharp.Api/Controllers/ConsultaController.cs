using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly ConsultaService _consultaService;

        public ConsultaController(ConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetAll()
        {
            var consultas = await _consultaService.GetAllAsync();
            return Ok(consultas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Consulta>> GetById(int id)
        {
            var consulta = await _consultaService.GetByIdAsync(id);
            if (consulta == null)
                return NotFound();
            return Ok(consulta);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Consulta consulta)
        {
            await _consultaService.AddAsync(consulta);
            return CreatedAtAction(nameof(GetById), new { id = consulta.ID_CONSULTA }, consulta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Consulta consulta)
        {
            if (id != consulta.ID_CONSULTA)
                return BadRequest("ID da consulta não corresponde ao informado na URL.");

            await _consultaService.UpdateAsync(consulta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var consulta = await _consultaService.GetByIdAsync(id);
            if (consulta == null)
                return NotFound();

            await _consultaService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("pacientes")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            var pacientes = await _consultaService.GetAllPacientesAsync();
            return Ok(pacientes);
        }

        [HttpGet("dentistas")]
        public async Task<ActionResult<IEnumerable<Dentista>>> GetDentistas()
        {
            var dentistas = await _consultaService.GetAllDentistasAsync();
            return Ok(dentistas);
        }
    }
}
