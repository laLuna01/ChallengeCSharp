using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Controllers;

public class DentistaController : Controller
{
    private readonly DentistaService _dentistaService;

    public DentistaController(DentistaService dentistaService)
    {
        _dentistaService = dentistaService;
    }

    public async Task<IActionResult> Index()
    {
        var dentistas = await _dentistaService.GetAllAsync();
        
        // Carregar paises para mostrar nomePais no ViewModel
        var dentistasVM = dentistas.Select(e => new DentistaViewModel
        {
            IdDentista = e.ID_DENTISTA,
            CRO = e.CRO,
            Especialidade = e.ESPECIALIDADE,
            Nome = e.NOME,
            IdEndereco = e.ENDERECO_ID_ENDERECO,
            LogradouroEndereco = e.Endereco?.LOGRADOURO,
            IdGenero = e.GENERO_ID_GENERO,
            NomeGenero = e.Genero?.DESCRICAO // Carregar com Include na query do repo para não ser null
        });

        return View(dentistasVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var generos = await _dentistaService.GetAllGenerosAsync();
        var enderecos = await _dentistaService.GetAllEnderecosAsync();

        var model = new DentistaViewModel
        {
            Generos = generos.Select(c => new SelectListItem(c.DESCRICAO, c.ID_GENERO.ToString())),
            Enderecos = enderecos.Select(c => new SelectListItem(c.LOGRADOURO, c.COD_ENDERECO.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DentistaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var generos = await _dentistaService.GetAllGenerosAsync();
            var enderecos = await _dentistaService.GetAllEnderecosAsync();
            model.Generos = generos.Select(c => new SelectListItem(c.DESCRICAO, c.ID_GENERO.ToString()));
            model.Enderecos = enderecos.Select(c => new SelectListItem(c.LOGRADOURO, c.COD_ENDERECO.ToString()));
            return View(model);
        }

        var dentista = new Dentista
        {
            NOME = model.Nome,
            ESPECIALIDADE = model.Especialidade,
            CRO = model.CRO,
            ENDERECO_ID_ENDERECO = model.IdEndereco,
            GENERO_ID_GENERO = model.IdGenero
        };

        await _dentistaService.AddAsync(dentista);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var dentista = await _dentistaService.GetByIdAsync(id);
        if (dentista == null)
            return NotFound();

        var generos = await _dentistaService.GetAllGenerosAsync();
        var enderecos = await _dentistaService.GetAllEnderecosAsync();

        var model = new DentistaViewModel
        {
            IdDentista = dentista.ID_DENTISTA,
            Nome = dentista.NOME,
            Especialidade = dentista.ESPECIALIDADE,
            CRO = dentista.CRO,
            IdGenero = dentista.GENERO_ID_GENERO,
            Generos = generos.Select(c => new SelectListItem(c.DESCRICAO, c.ID_GENERO.ToString())),
            IdEndereco = dentista.ENDERECO_ID_ENDERECO,
            Enderecos = enderecos.Select(c => new SelectListItem(c.LOGRADOURO, c.COD_ENDERECO.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DentistaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var generos = await _dentistaService.GetAllGenerosAsync();
            var enderecos = await _dentistaService.GetAllEnderecosAsync();
            model.Generos = generos.Select(c => new SelectListItem(c.DESCRICAO, c.ID_GENERO.ToString()));
            model.Enderecos = enderecos.Select(c => new SelectListItem(c.LOGRADOURO, c.COD_ENDERECO.ToString()));
            return View(model);
        }

        var dentista = await _dentistaService.GetByIdAsync(model.IdDentista);
        if (dentista == null)
            return NotFound();

        dentista.NOME = model.Nome;
        dentista.ESPECIALIDADE = model.Especialidade;
        dentista.CRO = model.CRO;
        dentista.GENERO_ID_GENERO = model.IdGenero;;
        dentista.ENDERECO_ID_ENDERECO = model.IdEndereco;

        await _dentistaService.UpdateAsync(dentista);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var dentista = await _dentistaService.GetByIdAsync(id);

        if (dentista == null)
            return NotFound();

        var model = new DentistaViewModel
        {
            IdDentista = dentista.ID_DENTISTA,
            Nome = dentista.NOME,
            Especialidade = dentista.ESPECIALIDADE,
            CRO = dentista.CRO,
            NomeGenero = dentista.Genero?.DESCRICAO,
            LogradouroEndereco = dentista.Endereco?.LOGRADOURO,
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var dentista = await _dentistaService.GetByIdAsync(id);
        if (dentista == null)
            return NotFound();

        await _dentistaService.DeleteAsync(dentista.ID_DENTISTA);
        return RedirectToAction(nameof(Index));
    }

}
