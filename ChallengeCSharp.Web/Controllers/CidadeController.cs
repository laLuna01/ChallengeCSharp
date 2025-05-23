using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Controllers;

public class CidadeController : Controller
{
    private readonly CidadeService _cidadeService;

    public CidadeController(CidadeService cidadeService)
    {
        _cidadeService = cidadeService;
    }

    public async Task<IActionResult> Index()
    {
        var cidades = await _cidadeService.GetAllAsync();
        
        // Carregar paises para mostrar nomePais no ViewModel
        var cidadesVM = cidades.Select(e => new CidadeViewModel
        {
            CodCidade = e.COD_CIDADE,
            NomeCidade = e.NOME,
            CodEstado = e.COD_ESTADO,
            NomeEstado = e.Estado?.NOME_ESTADO // Carregar com Include na query do repo para não ser null
        });

        return View(cidadesVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var estados = await _cidadeService.GetAllEstadosAsync();
        var model = new CidadeViewModel
        {
            Estados = estados.Select(e => new SelectListItem(e.NOME_ESTADO, e.COD_ESTADO.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CidadeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var estados = await _cidadeService.GetAllEstadosAsync();
            model.Estados = estados.Select(e => new SelectListItem(e.NOME_ESTADO, e.COD_ESTADO.ToString()));
            return View(model);
        }

        var cidade = new Cidade
        {
            NOME = model.NomeCidade,
            COD_ESTADO = model.CodEstado
        };

        await _cidadeService.AddAsync(cidade);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var cidade = await _cidadeService.GetByIdAsync(id);
        if (cidade == null)
            return NotFound();

        var estados = await _cidadeService.GetAllEstadosAsync();

        var model = new CidadeViewModel
        {
            CodCidade = cidade.COD_CIDADE,
            NomeCidade = cidade.NOME,
            CodEstado = cidade.COD_ESTADO,
            Estados = estados.Select(e => new SelectListItem(e.NOME_ESTADO, e.COD_ESTADO.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CidadeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var estados = await _cidadeService.GetAllEstadosAsync();
            model.Estados = estados.Select(e => new SelectListItem(e.NOME_ESTADO, e.COD_ESTADO.ToString()));
            return View(model);
        }

        var cidade = await _cidadeService.GetByIdAsync(model.CodCidade);
        if (cidade == null)
            return NotFound();

        cidade.NOME = model.NomeCidade;
        cidade.COD_ESTADO = model.CodEstado;

        await _cidadeService.UpdateAsync(cidade);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var cidade = await _cidadeService.GetByIdAsync(id);

        if (cidade == null)
            return NotFound();

        var model = new CidadeViewModel
        {
            CodCidade = cidade.COD_CIDADE,
            NomeCidade = cidade.NOME,
            NomeEstado = cidade.Estado?.NOME_ESTADO
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cidade = await _cidadeService.GetByIdAsync(id);
        if (cidade == null)
            return NotFound();

        await _cidadeService.DeleteAsync(cidade.COD_CIDADE);
        return RedirectToAction(nameof(Index));
    }

}
