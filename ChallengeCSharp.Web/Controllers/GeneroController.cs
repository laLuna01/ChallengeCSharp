using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace ChallengeCSharp.Web.Controllers
{
    public class GeneroController : Controller
    {
        private readonly GeneroService _generoService;

        public GeneroController(GeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var generos = await _generoService.GetAllAsync();
            
            // Se for usar ViewModel, converta aqui
            // Exemplo simples, retornando a entidade direto:
            return View(generos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GeneroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var genero = new Genero
            {
                DESCRICAO = model.Descricao
            };

            await _generoService.AddAsync(genero);
            
            // Redireciona para Index do próprio controller Genero
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var genero = await _generoService.GetByIdAsync(id);

            if (genero == null)
                return NotFound();

            var model = new GeneroViewModel
            {
                Id = genero.ID_GENERO,
                Descricao = genero.DESCRICAO
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GeneroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var genero = await _generoService.GetByIdAsync(model.Id);
            if (genero == null)
                return NotFound();

            genero.DESCRICAO = model.Descricao;

            await _generoService.UpdateAsync(genero);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var genero = await _generoService.GetByIdAsync(id);

            if (genero == null)
                return NotFound();

            var model = new GeneroViewModel
            {
                Id = genero.ID_GENERO,
                Descricao = genero.DESCRICAO
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genero = await _generoService.GetByIdAsync(id);
            if (genero == null)
                return NotFound();

            await _generoService.DeleteAsync(genero.ID_GENERO);

            return RedirectToAction(nameof(Index));
        }
    }
}