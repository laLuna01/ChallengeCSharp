using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        private readonly ApplicationDbContext _context;

        public BairroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bairro>> GetAllAsync() =>
            await _context.Bairros
                .Include(e => e.Cidade) 
                .ToListAsync();

        public async Task<Bairro?> GetByIdAsync(int id) =>
            await _context.Bairros
                .Include(e => e.Cidade)
                .FirstOrDefaultAsync(e => e.COD_BAIRRO == id);

        public async Task AddAsync(Bairro bairro)
        {
            _context.Bairros.Add(bairro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Bairro bairro)
        {
            _context.Bairros.Update(bairro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bairro = await GetByIdAsync(id);
            if (bairro != null)
            {
                _context.Bairros.Remove(bairro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Bairro>> GetByCidadeIdAsync(int cidadeId) =>
            await _context.Bairros
                .Include(e => e.Cidade)
                .Where(e => e.COD_CIDADE == cidadeId)
                .ToListAsync();
        
        public async Task<Bairro?> GetByNomeAsync(string nome, int codCidade)
        {
            return await _context.Bairros
                .Include(b => b.Cidade)
                .FirstOrDefaultAsync(b =>
                    b.NOME.ToLower() == nome.ToLower() &&
                    b.COD_CIDADE == codCidade);
        }

    }
}