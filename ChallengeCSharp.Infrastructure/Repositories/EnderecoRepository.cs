using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ApplicationDbContext _context;

        public EnderecoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Endereco>> GetAllAsync() =>
            await _context.Enderecos
                .Include(e => e.Bairro) 
                .ToListAsync();

        public async Task<Endereco?> GetByIdAsync(int id) =>
            await _context.Enderecos
                .Include(e => e.Bairro)
                .FirstOrDefaultAsync(e => e.COD_ENDERECO == id);

        public async Task AddAsync(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var endereco = await GetByIdAsync(id);
            if (endereco != null)
            {
                _context.Enderecos.Remove(endereco);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Endereco>> GetByBairroIdAsync(int bairroId) =>
            await _context.Enderecos
                .Include(e => e.Bairro)
                .Where(e => e.COD_BAIRRO == bairroId)
                .ToListAsync();
    }
}