using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Controllers;

public class PacienteController : Controller
{
    private readonly PacienteService _pacienteService;

    public PacienteController(PacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    public async Task<IActionResult> Index()
    {
        var pacientes = await _pacienteService.GetAllAsync();
        
        // Carregar paises para mostrar nomePais no ViewModel
        var pacientesVM = pacientes.Select(e => new PacienteViewModel
        {
            IdPaciente = e.ID_PACIENTE,
            CPF = e.CPF,
            DataNascimento = e.DATA_NASCIMENTO,
            Nome = e.NOME,
            IdEndereco = e.ENDERECO_ID_ENDERECO,
            LogradouroEndereco = e.Endereco?.LOGRADOURO,
            IdGenero = e.GENERO_ID_GENERO,
            NomeGenero = e.Genero?.DESCRICAO // Carregar com Include na query do repo para não ser null
        });

        return View(pacientesVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var generos = await _pacienteService.GetAllGenerosAsync();
        var enderecos = await _pacienteService.GetAllEnderecosAsync();

        var model = new PacienteViewModel
        {
            Generos = generos.Select(c => new SelectListItem(c.DESCRICAO, c.ID_GENERO.ToString())),
            Enderecos = enderecos.Select(c => new SelectListItem(c.LOGRADOURO, c.COD_ENDERECO.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PacienteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var generos = await _pacienteService.GetAllGenerosAsync();
            var enderecos = await _pacienteService.GetAllEnderecosAsync();
            model.Generos = generos.Select(c => new SelectListItem(c.DESCRICAO, c.ID_GENERO.ToString()));
            model.Enderecos = enderecos.Select(c => new SelectListItem(c.LOGRADOURO, c.COD_ENDERECO.ToString()));
            return View(model);
        }

        var paciente = new Paciente
        {
            NOME = model.Nome,
            DATA_NASCIMENTO = model.DataNascimento,
            CPF = model.CPF,
            ENDERECO_ID_ENDERECO = model.IdEndereco,
            GENERO_ID_GENERO = model.IdGenero
        };

        await _pacienteService.AddAsync(paciente);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);
        if (paciente == null)
            return NotFound();

        var generos = await _pacienteService.GetAllGenerosAsync();
        var enderecos = await _pacienteService.GetAllEnderecosAsync();

        var model = new PacienteViewModel
        {
            IdPaciente = paciente.ID_PACIENTE,
            Nome = paciente.NOME,
            DataNascimento = paciente.DATA_NASCIMENTO,
            CPF = paciente.CPF,
            IdGenero = paciente.GENERO_ID_GENERO,
            Generos = generos.Select(c => new SelectListItem(c.DESCRICAO, c.ID_GENERO.ToString())),
            IdEndereco = paciente.ENDERECO_ID_ENDERECO,
            Enderecos = enderecos.Select(c => new SelectListItem(c.LOGRADOURO, c.COD_ENDERECO.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PacienteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var generos = await _pacienteService.GetAllGenerosAsync();
            var enderecos = await _pacienteService.GetAllEnderecosAsync();
            model.Generos = generos.Select(c => new SelectListItem(c.DESCRICAO, c.ID_GENERO.ToString()));
            model.Enderecos = enderecos.Select(c => new SelectListItem(c.LOGRADOURO, c.COD_ENDERECO.ToString()));
            return View(model);
        }

        var paciente = await _pacienteService.GetByIdAsync(model.IdPaciente);
        if (paciente == null)
            return NotFound();

        paciente.NOME = model.Nome;
        paciente.DATA_NASCIMENTO = model.DataNascimento;
        paciente.CPF = model.CPF;
        paciente.GENERO_ID_GENERO = model.IdGenero;;
        paciente.ENDERECO_ID_ENDERECO = model.IdEndereco;

        await _pacienteService.UpdateAsync(paciente);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);

        if (paciente == null)
            return NotFound();

        var model = new PacienteViewModel
        {
            IdPaciente = paciente.ID_PACIENTE,
            Nome = paciente.NOME,
            DataNascimento = paciente.DATA_NASCIMENTO,
            CPF = paciente.CPF,
            NomeGenero = paciente.Genero?.DESCRICAO,
            LogradouroEndereco = paciente.Endereco?.LOGRADOURO,
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);
        if (paciente == null)
            return NotFound();

        await _pacienteService.DeleteAsync(paciente.ID_PACIENTE);
        return RedirectToAction(nameof(Index));
    }

}
