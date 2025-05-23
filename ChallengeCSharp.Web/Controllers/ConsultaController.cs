using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Controllers;

public class ConsultaController : Controller
{
    private readonly ConsultaService _consultaService;

    public ConsultaController(ConsultaService consultaService)
    {
        _consultaService = consultaService;
    }

    public async Task<IActionResult> Index()
    {
        var consultas = await _consultaService.GetAllAsync();
        
        // Carregar paises para mostrar nomePais no ViewModel
        var consultasVM = consultas.Select(e => new ConsultaViewModel
        {
            IdConsulta = e.ID_CONSULTA,
            Custo = e.CUSTO,
            DataConsulta = e.DATA_CONSULTA,
            StatusSinistro = e.STATUS_SINISTRO,
            TipoConsulta = e.TIPO_CONSULTA,
            IdDentista = e.DENTISTA_ID_DENTISTA,
            NomeDentista = e.Dentista?.NOME,
            IdPaciente = e.PACIENTE_ID_PACIENTE,
            NomePaciente = e.Paciente?.NOME // Carregar com Include na query do repo para não ser null
        });

        return View(consultasVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var pacientes = await _consultaService.GetAllPacientesAsync();
        var dentistas = await _consultaService.GetAllDentistasAsync();

        var model = new ConsultaViewModel
        {
            Pacientes = pacientes.Select(c => new SelectListItem(c.NOME, c.ID_PACIENTE.ToString())),
            Dentistas = dentistas.Select(c => new SelectListItem(c.NOME, c.ID_DENTISTA.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ConsultaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var pacientes = await _consultaService.GetAllPacientesAsync();
            var dentistas = await _consultaService.GetAllDentistasAsync();
            model.Pacientes = pacientes.Select(c => new SelectListItem(c.NOME, c.ID_PACIENTE.ToString()));
            model.Dentistas = dentistas.Select(c => new SelectListItem(c.NOME, c.ID_DENTISTA.ToString()));
            return View(model);
        }

        var consulta = new Consulta
        {
            CUSTO = model.Custo,
            DATA_CONSULTA = model.DataConsulta,
            STATUS_SINISTRO = model.StatusSinistro,
            TIPO_CONSULTA = model.TipoConsulta,
            PACIENTE_ID_PACIENTE = model.IdPaciente,
            DENTISTA_ID_DENTISTA = model.IdDentista
        };

        await _consultaService.AddAsync(consulta);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var consulta = await _consultaService.GetByIdAsync(id);
        if (consulta == null)
            return NotFound();
        
        var pacientes = await _consultaService.GetAllPacientesAsync();
        var dentistas = await _consultaService.GetAllDentistasAsync();

        var model = new ConsultaViewModel
        {
            IdConsulta = consulta.ID_CONSULTA,
            Custo = consulta.CUSTO,
            DataConsulta = consulta.DATA_CONSULTA,
            StatusSinistro = consulta.STATUS_SINISTRO,
            TipoConsulta = consulta.TIPO_CONSULTA,
            IdDentista = consulta.DENTISTA_ID_DENTISTA,
            Dentistas = dentistas.Select(c => new SelectListItem(c.NOME, c.ID_DENTISTA.ToString())),
            IdPaciente = consulta.PACIENTE_ID_PACIENTE,
            Pacientes = pacientes.Select(c => new SelectListItem(c.NOME, c.ID_PACIENTE.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ConsultaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var pacientes = await _consultaService.GetAllPacientesAsync();
            var dentistas = await _consultaService.GetAllDentistasAsync();
            model.Pacientes = pacientes.Select(c => new SelectListItem(c.NOME, c.ID_PACIENTE.ToString()));
            model.Dentistas = dentistas.Select(c => new SelectListItem(c.NOME, c.ID_DENTISTA.ToString()));
            return View(model);
        }

        var consulta = await _consultaService.GetByIdAsync(model.IdConsulta);
        if (consulta == null)
            return NotFound();

        consulta.CUSTO = model.Custo;
        consulta.DATA_CONSULTA = model.DataConsulta;
        consulta.STATUS_SINISTRO = model.StatusSinistro;
        consulta.TIPO_CONSULTA = model.TipoConsulta;
        consulta.DENTISTA_ID_DENTISTA = model.IdDentista;;
        consulta.PACIENTE_ID_PACIENTE = model.IdPaciente;

        await _consultaService.UpdateAsync(consulta);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var consulta = await _consultaService.GetByIdAsync(id);

        if (consulta == null)
            return NotFound();

        var model = new ConsultaViewModel
        {
            IdConsulta = consulta.ID_CONSULTA,
            Custo = consulta.CUSTO,
            DataConsulta = consulta.DATA_CONSULTA,
            StatusSinistro = consulta.STATUS_SINISTRO,
            TipoConsulta = consulta.TIPO_CONSULTA,
            NomeDentista = consulta.Dentista?.NOME,
            NomePaciente = consulta.Paciente?.NOME,
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var consulta = await _consultaService.GetByIdAsync(id);
        if (consulta == null)
            return NotFound();

        await _consultaService.DeleteAsync(consulta.ID_CONSULTA);
        return RedirectToAction(nameof(Index));
    }

}
