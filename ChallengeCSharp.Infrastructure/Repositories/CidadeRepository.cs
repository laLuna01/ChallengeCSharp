using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly ApplicationDbContext _context;

        public CidadeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cidade>> GetAllAsync() =>
            await _context.Cidades
                .Include(e => e.Estado) 
                .ToListAsync();

        public async Task<Cidade?> GetByIdAsync(int id) =>
            await _context.Cidades
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(e => e.COD_CIDADE == id);

        public async Task AddAsync(Cidade cidade)
        {
            _context.Cidades.Add(cidade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cidade cidade)
        {
            _context.Cidades.Update(cidade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cidade = await GetByIdAsync(id);
            if (cidade != null)
            {
                _context.Cidades.Remove(cidade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Cidade>> GetByEstadoIdAsync(int estadoId) =>
            await _context.Cidades
                .Include(e => e.Estado)
                .Where(e => e.COD_ESTADO == estadoId)
                .ToListAsync();
        
        public async Task<Cidade?> GetByNomeAsync(string nome, int codEstado)
        {
            return await _context.Cidades
                .Include(c => c.Estado)
                .FirstOrDefaultAsync(c =>
                    c.NOME.ToLower() == nome.ToLower() &&
                    c.COD_ESTADO == codEstado);
        }

    }
}