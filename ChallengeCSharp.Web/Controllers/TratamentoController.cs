using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Controllers;

public class TratamentoController : Controller
{
    private readonly TratamentoService _tratamentoService;

    public TratamentoController(TratamentoService tratamentoService)
    {
        _tratamentoService = tratamentoService;
    }

    public async Task<IActionResult> Index()
    {
        var tratamentos = await _tratamentoService.GetAllAsync();
        
        // Carregar paises para mostrar nomePais no ViewModel
        var tratamentosVM = tratamentos.Select(e => new TratamentoViewModel
        {
            IdTratamento = e.ID_TRATAMENTO,
            Custo = e.CUSTO,
            DataInicio = e.DATA_INICIO,
            DataTermino = e.DATA_TERMINO,
            Descricao = e.DESCRICAO,
            TipoTratamento = e.TIPO_TRATAMENTO,
            IdConsulta = e.CONSULTA_ID_CONSULTA
        });

        return View(tratamentosVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var consultas = await _tratamentoService.GetAllConsultasAsync();

        var model = new TratamentoViewModel
        {
            Consultas = consultas.Select(c => new SelectListItem(c.TIPO_CONSULTA + " " + c.ID_CONSULTA, c.ID_CONSULTA.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TratamentoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var consultas = await _tratamentoService.GetAllConsultasAsync();
            model.Consultas = consultas.Select(c => new SelectListItem(c.TIPO_CONSULTA + " " + c.ID_CONSULTA, c.ID_CONSULTA.ToString()));
            return View(model);
        }

        var tratamento = new Tratamento
        {
            CUSTO = model.Custo,
            DATA_INICIO = model.DataInicio,
            DATA_TERMINO = model.DataTermino,
            DESCRICAO = model.Descricao,
            TIPO_TRATAMENTO = model.TipoTratamento,
            CONSULTA_ID_CONSULTA = model.IdConsulta
        };

        await _tratamentoService.AddAsync(tratamento);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var tratamento = await _tratamentoService.GetByIdAsync(id);
        if (tratamento == null)
            return NotFound();
        
        var consultas = await _tratamentoService.GetAllConsultasAsync();

        var model = new TratamentoViewModel
        {
            IdTratamento = tratamento.ID_TRATAMENTO,
            Custo = tratamento.CUSTO,
            DataInicio = tratamento.DATA_INICIO,
            DataTermino = tratamento.DATA_TERMINO,
            Descricao = tratamento.DESCRICAO,
            TipoTratamento = tratamento.TIPO_TRATAMENTO,
            IdConsulta = tratamento.CONSULTA_ID_CONSULTA,
            Consultas = consultas.Select(c => new SelectListItem(c.TIPO_CONSULTA + " " + c.ID_CONSULTA, c.ID_CONSULTA.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TratamentoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var consultas = await _tratamentoService.GetAllConsultasAsync();
            model.Consultas = consultas.Select(c => new SelectListItem(c.TIPO_CONSULTA + " " + c.ID_CONSULTA, c.ID_CONSULTA.ToString()));
            return View(model);
        }

        var tratamento = await _tratamentoService.GetByIdAsync(model.IdTratamento);
        if (tratamento == null)
            return NotFound();

        tratamento.CUSTO = model.Custo;
        tratamento.DATA_INICIO = model.DataInicio;
        tratamento.DATA_TERMINO = model.DataTermino;
        tratamento.DESCRICAO = model.Descricao;
        tratamento.TIPO_TRATAMENTO = model.TipoTratamento;
        tratamento.CONSULTA_ID_CONSULTA = model.IdConsulta;

        await _tratamentoService.UpdateAsync(tratamento);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var tratamento = await _tratamentoService.GetByIdAsync(id);

        if (tratamento == null)
            return NotFound();

        var model = new TratamentoViewModel
        {
            IdTratamento = tratamento.ID_TRATAMENTO,
            Custo = tratamento.CUSTO,
            DataInicio = tratamento.DATA_INICIO,
            DataTermino = tratamento.DATA_TERMINO,
            Descricao = tratamento.DESCRICAO,
            TipoTratamento = tratamento.TIPO_TRATAMENTO,
            IdConsulta = tratamento.CONSULTA_ID_CONSULTA,
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tratamento = await _tratamentoService.GetByIdAsync(id);
        if (tratamento == null)
            return NotFound();

        await _tratamentoService.DeleteAsync(tratamento.ID_TRATAMENTO);
        return RedirectToAction(nameof(Index));
    }

}
