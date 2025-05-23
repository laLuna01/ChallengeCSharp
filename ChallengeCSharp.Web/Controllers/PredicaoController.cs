using ChallengeCSharp.Domain.ML;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCSharp.Web.Controllers;

public class PredicaoController : Controller
{
    private readonly SinistroIAService _service;

    public PredicaoController(SinistroIAService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new PredicaoViewModel());
    }

    [HttpPost]
    public IActionResult Index(PredicaoViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var entrada = new SinistroData
        {
            Valor = model.Valor,
            Procedimento = model.Procedimento,
            HistoricoNegativo = model.HistoricoNegativo
        };

        var resultado = _service.PreverAprovacao(entrada);

        model.Aprovado = resultado.Aprovado;
        model.Probabilidade = resultado.Probability;
        model.Score = resultado.Score;

        return View(model);
    }
}