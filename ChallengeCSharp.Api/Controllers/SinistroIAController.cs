using ChallengeCSharp.Domain.ML;
using Microsoft.AspNetCore.Mvc;
using ChallengeCSharp.Application.Services;

namespace ChallengeCSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinistroIAController : ControllerBase
    {
        private readonly SinistroIAService _service;

        public SinistroIAController(SinistroIAService service)
        {
            _service = service;
        }

        [HttpPost("prever")]
        public IActionResult Prever([FromBody] SinistroData input)
        {
            var resultado = _service.PreverAprovacao(input);
            return Ok(resultado);
        }
    }
}