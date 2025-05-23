using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChallengeCSharp.Web.Controllers
{
    public class PaisController : Controller
    {
        private readonly PaisService _paisService;

        public PaisController(PaisService paisService)
        {
            _paisService = paisService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var paises = await _paisService.GetAllAsync();
            return View(paises);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaisViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var pais = new Pais
            {
                NOME = model.Nome
            };

            await _paisService.AddAsync(pais);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var pais = await _paisService.GetByIdAsync(id);

            if (pais == null)
                return NotFound();

            var model = new PaisViewModel
            {
                Id = pais.COD_PAIS,
                Nome = pais.NOME
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PaisViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var pais = await _paisService.GetByIdAsync(model.Id);
            if (pais == null)
                return NotFound();

            pais.NOME = model.Nome;
            await _paisService.UpdateAsync(pais);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var pais = await _paisService.GetByIdAsync(id);

            if (pais == null)
                return NotFound();

            var model = new PaisViewModel
            {
                Id = pais.COD_PAIS,
                Nome = pais.NOME
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pais = await _paisService.GetByIdAsync(id);
            if (pais == null)
                return NotFound();

            await _paisService.DeleteAsync(pais.COD_PAIS);
            return RedirectToAction(nameof(Index));
        }
    }
}
