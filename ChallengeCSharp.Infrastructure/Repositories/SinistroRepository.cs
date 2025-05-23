using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class SinistroRepository : ISinistroRepository
    {
        private readonly ApplicationDbContext _context;

        public SinistroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sinistro>> GetAllAsync() =>
            await _context.Sinistros
                .Include(e => e.Consulta) 
                .ToListAsync();

        public async Task<Sinistro?> GetByIdAsync(int id) =>
            await _context.Sinistros
                .Include(e => e.Consulta) 
                .FirstOrDefaultAsync(e => e.ID_SINISTRO == id);

        public async Task AddAsync(Sinistro sinistro)
        {
            _context.Sinistros.Add(sinistro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sinistro sinistro)
        {
            _context.Sinistros.Update(sinistro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sinistro = await GetByIdAsync(id);
            if (sinistro != null)
            {
                _context.Sinistros.Remove(sinistro);
                await _context.SaveChangesAsync();
            }
        }
    }
}