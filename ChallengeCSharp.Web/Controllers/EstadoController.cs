using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Controllers;

public class EstadoController : Controller
{
    private readonly EstadoService _estadoService;

    public EstadoController(EstadoService estadoService)
    {
        _estadoService = estadoService;
    }

    public async Task<IActionResult> Index()
    {
        var estados = await _estadoService.GetAllAsync();
        
        // Carregar paises para mostrar nomePais no ViewModel
        var estadosVM = estados.Select(e => new EstadoViewModel
        {
            CodEstado = e.COD_ESTADO,
            NomeEstado = e.NOME_ESTADO,
            CodPais = e.COD_PAIS,
            NomePais = e.Pais?.NOME // Carregar com Include na query do repo para não ser null
        });

        return View(estadosVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var paises = await _estadoService.GetAllPaisesAsync();
        var model = new EstadoViewModel
        {
            Paises = paises.Select(p => new SelectListItem(p.NOME, p.COD_PAIS.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EstadoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var paises = await _estadoService.GetAllPaisesAsync();
            model.Paises = paises.Select(p => new SelectListItem(p.NOME, p.COD_PAIS.ToString()));
            return View(model);
        }

        var estado = new Estado
        {
            NOME_ESTADO = model.NomeEstado,
            COD_PAIS = model.CodPais
        };

        await _estadoService.AddAsync(estado);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var estado = await _estadoService.GetByIdAsync(id);
        if (estado == null)
            return NotFound();

        var paises = await _estadoService.GetAllPaisesAsync();

        var model = new EstadoViewModel
        {
            CodEstado = estado.COD_ESTADO,
            NomeEstado = estado.NOME_ESTADO,
            CodPais = estado.COD_PAIS,
            Paises = paises.Select(p => new SelectListItem(p.NOME, p.COD_PAIS.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EstadoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var paises = await _estadoService.GetAllPaisesAsync();
            model.Paises = paises.Select(p => new SelectListItem(p.NOME, p.COD_PAIS.ToString()));
            return View(model);
        }

        var estado = await _estadoService.GetByIdAsync(model.CodEstado);
        if (estado == null)
            return NotFound();

        estado.NOME_ESTADO = model.NomeEstado;
        estado.COD_PAIS = model.CodPais;

        await _estadoService.UpdateAsync(estado);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var estado = await _estadoService.GetByIdAsync(id);

        if (estado == null)
            return NotFound();

        var model = new EstadoViewModel
        {
            CodEstado = estado.COD_ESTADO,
            NomeEstado = estado.NOME_ESTADO,
            NomePais = estado.Pais?.NOME
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var estado = await _estadoService.GetByIdAsync(id);
        if (estado == null)
            return NotFound();

        await _estadoService.DeleteAsync(estado.COD_ESTADO);
        return RedirectToAction(nameof(Index));
    }

}
