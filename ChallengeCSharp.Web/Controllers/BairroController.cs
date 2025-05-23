using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Controllers;

public class BairroController : Controller
{
    private readonly BairroService _bairroService;

    public BairroController(BairroService bairroService)
    {
        _bairroService = bairroService;
    }

    public async Task<IActionResult> Index()
    {
        var bairros = await _bairroService.GetAllAsync();
        
        // Carregar paises para mostrar nomePais no ViewModel
        var bairrosVM = bairros.Select(e => new BairroViewModel
        {
            CodBairro = e.COD_BAIRRO,
            NomeBairro = e.NOME,
            CodCidade = e.COD_CIDADE,
            NomeCidade = e.Cidade?.NOME // Carregar com Include na query do repo para não ser null
        });

        return View(bairrosVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var cidades = await _bairroService.GetAllCidadesAsync();
        var model = new BairroViewModel
        {
            Cidades = cidades.Select(c => new SelectListItem(c.NOME, c.COD_CIDADE.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BairroViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var cidades = await _bairroService.GetAllCidadesAsync();
            model.Cidades = cidades.Select(c => new SelectListItem(c.NOME, c.COD_CIDADE.ToString()));
            return View(model);
        }

        var bairro = new Bairro
        {
            NOME = model.NomeBairro,
            COD_CIDADE = model.CodCidade
        };

        await _bairroService.AddAsync(bairro);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var bairro = await _bairroService.GetByIdAsync(id);
        if (bairro == null)
            return NotFound();

        var cidades = await _bairroService.GetAllCidadesAsync();

        var model = new BairroViewModel
        {
            CodBairro = bairro.COD_BAIRRO,
            NomeBairro = bairro.NOME,
            CodCidade = bairro.COD_CIDADE,
            Cidades = cidades.Select(c => new SelectListItem(c.NOME, c.COD_CIDADE.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BairroViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var cidades = await _bairroService.GetAllCidadesAsync();
            model.Cidades = cidades.Select(c => new SelectListItem(c.NOME, c.COD_CIDADE.ToString()));
            return View(model);
        }

        var bairro = await _bairroService.GetByIdAsync(model.CodBairro);
        if (bairro == null)
            return NotFound();

        bairro.NOME = model.NomeBairro;
        bairro.COD_CIDADE = model.CodCidade;

        await _bairroService.UpdateAsync(bairro);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var bairro = await _bairroService.GetByIdAsync(id);

        if (bairro == null)
            return NotFound();

        var model = new BairroViewModel
        {
            CodBairro = bairro.COD_BAIRRO,
            NomeBairro = bairro.NOME,
            NomeCidade = bairro.Cidade?.NOME
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bairro = await _bairroService.GetByIdAsync(id);
        if (bairro == null)
            return NotFound();

        await _bairroService.DeleteAsync(bairro.COD_BAIRRO);
        return RedirectToAction(nameof(Index));
    }

}
