using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class DentistaRepository : IDentistaRepository
    {
        private readonly ApplicationDbContext _context;

        public DentistaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dentista>> GetAllAsync() =>
            await _context.Dentistas
                .Include(e => e.Genero) 
                .Include(e => e.Endereco)
                .ToListAsync();

        public async Task<Dentista?> GetByIdAsync(int id) =>
            await _context.Dentistas
                .Include(e => e.Genero) 
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(e => e.ID_DENTISTA == id);

        public async Task AddAsync(Dentista dentista)
        {
            _context.Dentistas.Add(dentista);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Dentista dentista)
        {
            _context.Dentistas.Update(dentista);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dentista = await GetByIdAsync(id);
            if (dentista != null)
            {
                _context.Dentistas.Remove(dentista);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Dentista>> GetByGeneroIdAsync(int generoId) =>
            await _context.Dentistas
                .Include(e => e.Endereco)
                .Include(e => e.Genero)
                .Where(e => e.GENERO_ID_GENERO == generoId)
                .ToListAsync();
    }
}