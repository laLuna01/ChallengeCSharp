using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class TratamentoRepository : ITratamentoRepository
    {
        private readonly ApplicationDbContext _context;

        public TratamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tratamento>> GetAllAsync() =>
            await _context.Tratamentos
                .Include(e => e.Consulta) 
                .ToListAsync();

        public async Task<Tratamento?> GetByIdAsync(int id) =>
            await _context.Tratamentos
                .Include(e => e.Consulta) 
                .FirstOrDefaultAsync(e => e.ID_TRATAMENTO == id);

        public async Task AddAsync(Tratamento tratamento)
        {
            _context.Tratamentos.Add(tratamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tratamento tratamento)
        {
            _context.Tratamentos.Update(tratamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tratamento = await GetByIdAsync(id);
            if (tratamento != null)
            {
                _context.Tratamentos.Remove(tratamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}