using ChallengeCSharp.Application.DTOs;
using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _service;

        public EnderecoController(EnderecoService service)
        {
            _service = service;
        }

        // GET: api/endereco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> Get()
        {
            var enderecos = await _service.GetAllAsync();
            return Ok(enderecos);
        }

        // GET: api/endereco/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> Get(int id)
        {
            var endereco = await _service.GetByIdAsync(id);
            if (endereco == null)
                return NotFound();

            return Ok(endereco);
        }

        // POST: api/endereco
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Endereco endereco)
        {
            await _service.AddAsync(endereco);
            return CreatedAtAction(nameof(Get), new { id = endereco.COD_ENDERECO }, endereco);
        }

        // PUT: api/endereco/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Endereco endereco)
        {
            if (id != endereco.COD_ENDERECO)
                return BadRequest();

            await _service.UpdateAsync(endereco);
            return NoContent();
        }

        // DELETE: api/endereco/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/endereco/bairros
        [HttpGet("bairros")]
        public async Task<ActionResult<IEnumerable<Bairro>>> GetBairros()
        {
            var bairros = await _service.GetAllBairrosAsync();
            return Ok(bairros);
        }
        
        // POST: api/endereco/criar-por-cep
        [HttpPost("criar-por-cep")]
        public async Task<IActionResult> CriarEnderecoPorCep([FromBody] EnderecoCepDto dto)
        {
            var endereco = await _service.ObterEnderecoPorCepAsync(dto.Cep);

            if (endereco == null)
                return NotFound("Endereço não encontrado para o CEP informado.");

            endereco.NUMERO = dto.Numero;
            endereco.REFERENCIA = dto.Referencia ?? string.Empty;

            await _service.AddAsync(endereco);

            return Ok(endereco);
        }
    }
}
