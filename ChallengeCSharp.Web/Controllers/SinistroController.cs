using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Controllers;

public class SinistroController : Controller
{
    private readonly SinistroService _sinistroService;

    public SinistroController(SinistroService sinistroService)
    {
        _sinistroService = sinistroService;
    }

    public async Task<IActionResult> Index()
    {
        var sinistros = await _sinistroService.GetAllAsync();
        
        // Carregar paises para mostrar nomePais no ViewModel
        var sinistrosVM = sinistros.Select(e => new SinistroViewModel
        {
            IdSinistro = e.ID_SINISTRO,
            DataAbertura = e.DATA_ABERTURA,
            Descricao = e.DESCRICAO_SINISTRO,
            Motivo = e.MOTIVO_SINISTRO,
            Status = e.STATUS_SINISTRO,
            IdConsulta = e.CONSULTA_ID_CONSULTA
        });

        return View(sinistrosVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var consultas = await _sinistroService.GetAllConsultasAsync();

        var model = new SinistroViewModel
        {
            Consultas = consultas.Select(c => new SelectListItem(c.TIPO_CONSULTA + " " + c.ID_CONSULTA, c.ID_CONSULTA.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SinistroViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var consultas = await _sinistroService.GetAllConsultasAsync();
            model.Consultas = consultas.Select(c => new SelectListItem(c.TIPO_CONSULTA + " " + c.ID_CONSULTA, c.ID_CONSULTA.ToString()));
            return View(model);
        }

        var sinistro = new Sinistro
        {
            DATA_ABERTURA = model.DataAbertura,
            DESCRICAO_SINISTRO = model.Descricao,
            MOTIVO_SINISTRO = model.Motivo,
            STATUS_SINISTRO = model.Status,
            CONSULTA_ID_CONSULTA = model.IdConsulta
        };

        await _sinistroService.AddAsync(sinistro);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var sinistro = await _sinistroService.GetByIdAsync(id);
        if (sinistro == null)
            return NotFound();
        
        var consultas = await _sinistroService.GetAllConsultasAsync();

        var model = new SinistroViewModel
        {
            IdSinistro = sinistro.ID_SINISTRO,
            DataAbertura = sinistro.DATA_ABERTURA,
            Descricao = sinistro.DESCRICAO_SINISTRO,
            Motivo = sinistro.MOTIVO_SINISTRO,
            Status = sinistro.STATUS_SINISTRO,
            IdConsulta = sinistro.CONSULTA_ID_CONSULTA,
            Consultas = consultas.Select(c => new SelectListItem(c.TIPO_CONSULTA + " " + c.ID_CONSULTA, c.ID_CONSULTA.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SinistroViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var consultas = await _sinistroService.GetAllConsultasAsync();
            model.Consultas = consultas.Select(c => new SelectListItem(c.TIPO_CONSULTA + " " + c.ID_CONSULTA, c.ID_CONSULTA.ToString()));
            return View(model);
        }

        var sinistro = await _sinistroService.GetByIdAsync(model.IdSinistro);
        if (sinistro == null)
            return NotFound();

        sinistro.DATA_ABERTURA = model.DataAbertura;
        sinistro.DESCRICAO_SINISTRO = model.Descricao;
        sinistro.MOTIVO_SINISTRO = model.Motivo;
        sinistro.STATUS_SINISTRO = model.Status;
        sinistro.CONSULTA_ID_CONSULTA = model.IdConsulta;

        await _sinistroService.UpdateAsync(sinistro);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var sinistro = await _sinistroService.GetByIdAsync(id);

        if (sinistro == null)
            return NotFound();

        var model = new SinistroViewModel
        {
            IdSinistro = sinistro.ID_SINISTRO,
            DataAbertura = sinistro.DATA_ABERTURA,
            Descricao = sinistro.DESCRICAO_SINISTRO,
            Motivo = sinistro.MOTIVO_SINISTRO,
            Status = sinistro.STATUS_SINISTRO,
            IdConsulta = sinistro.CONSULTA_ID_CONSULTA,
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sinistro = await _sinistroService.GetByIdAsync(id);
        if (sinistro == null)
            return NotFound();

        await _sinistroService.DeleteAsync(sinistro.ID_SINISTRO);
        return RedirectToAction(nameof(Index));
    }

}
