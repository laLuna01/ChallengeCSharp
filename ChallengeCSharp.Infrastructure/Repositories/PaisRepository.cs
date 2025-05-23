using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class PaisRepository : IPaisRepository
    {
        private readonly ApplicationDbContext _context;

        public PaisRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pais>> GetAllAsync() =>
            await _context.Paises.ToListAsync();

        public async Task<Pais?> GetByIdAsync(int id) =>
            await _context.Paises.FindAsync(id);

        public async Task AddAsync(Pais pais)
        {
            _context.Paises.Add(pais);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pais pais)
        {
            _context.Paises.Update(pais);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pais = await GetByIdAsync(id);
            if (pais is not null)
            {
                _context.Paises.Remove(pais);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<Pais?> GetByNomeAsync(string nome) =>
            await _context.Paises
                .FirstOrDefaultAsync(e => e.NOME.ToLower() == nome.ToLower());
    }
}