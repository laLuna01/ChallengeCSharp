using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Controllers;

public class EnderecoController : Controller
{
    private readonly EnderecoService _enderecoService;

    public EnderecoController(EnderecoService enderecoService)
    {
        _enderecoService = enderecoService;
    }

    public async Task<IActionResult> Index()
    {
        var enderecos = await _enderecoService.GetAllAsync();
        
        // Carregar paises para mostrar nomePais no ViewModel
        var enderecosVM = enderecos.Select(e => new EnderecoViewModel
        {
            CodEndereco = e.COD_ENDERECO,
            Logradouro = e.LOGRADOURO,
            Numero = e.NUMERO,
            Referencia = e.REFERENCIA,
            CEP = e.CEP.ToString(),
            CodBairro = e.COD_BAIRRO,
            NomeBairro = e.Bairro?.NOME // Carregar com Include na query do repo para não ser null
        });

        return View(enderecosVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var bairros = await _enderecoService.GetAllBairrosAsync();
        var model = new EnderecoViewModel
        {
            Bairros = bairros.Select(c => new SelectListItem(c.NOME, c.COD_BAIRRO.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EnderecoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Erro no campo {modelState.Key}: {error.ErrorMessage}");
                }
            }

            return View(model);
        }
        
        Console.WriteLine($"CEP: {model.CEP}");
        var enderecoServicoResult = await _enderecoService.ObterEnderecoPorCepAsync(model.CEP.ToString());

        if (enderecoServicoResult == null)
        {
            ModelState.AddModelError(nameof(model.CEP), "CEP não encontrado.");
            return View(model);
        }
        
        model.Logradouro = enderecoServicoResult.LOGRADOURO;
        model.CodBairro = enderecoServicoResult.COD_BAIRRO;

        var endereco = new Endereco
        {
            LOGRADOURO = model.Logradouro,
            REFERENCIA = model.Referencia,
            CEP = Convert.ToInt32(model.CEP),
            NUMERO = model.Numero,
            COD_BAIRRO = model.CodBairro
        };

        await _enderecoService.AddAsync(endereco);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var endereco = await _enderecoService.GetByIdAsync(id);
        if (endereco == null)
            return NotFound();

        var bairros = await _enderecoService.GetAllBairrosAsync();

        var model = new EnderecoViewModel
        {
            CodEndereco = endereco.COD_ENDERECO,
            Logradouro = endereco.LOGRADOURO,
            Numero = endereco.NUMERO,
            Referencia = endereco.REFERENCIA,
            CEP = endereco.CEP.ToString(),
            CodBairro = endereco.COD_BAIRRO,
            Bairros = bairros.Select(c => new SelectListItem(c.NOME, c.COD_BAIRRO.ToString()))
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EnderecoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var bairros = await _enderecoService.GetAllBairrosAsync();
            model.Bairros = bairros.Select(c => new SelectListItem(c.NOME, c.COD_BAIRRO.ToString()));
            return View(model);
        }

        var endereco = await _enderecoService.GetByIdAsync(model.CodEndereco);
        if (endereco == null)
            return NotFound();

        endereco.LOGRADOURO = model.Logradouro;
        endereco.REFERENCIA = model.Referencia;
        endereco.CEP = Convert.ToInt32(model.CEP);
        endereco.NUMERO = model.Numero;
        endereco.COD_BAIRRO = model.CodBairro;

        await _enderecoService.UpdateAsync(endereco);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var endereco = await _enderecoService.GetByIdAsync(id);

        if (endereco == null)
            return NotFound();

        var model = new EnderecoViewModel
        {
            CodEndereco = endereco.COD_ENDERECO,
            Logradouro = endereco.LOGRADOURO,
            Numero = endereco.NUMERO,
            Referencia = endereco.REFERENCIA,
            CEP = endereco.CEP.ToString(),
            NomeBairro = endereco.Bairro?.NOME
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var endereco = await _enderecoService.GetByIdAsync(id);
        if (endereco == null)
            return NotFound();

        await _enderecoService.DeleteAsync(endereco.COD_ENDERECO);
        return RedirectToAction(nameof(Index));
    }

}
