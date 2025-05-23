using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly ApplicationDbContext _context;

        public EstadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estado>> GetAllAsync() =>
            await _context.Estados
                .Include(e => e.Pais)  // inclui a entidade relacionada Pais
                .ToListAsync();

        public async Task<Estado?> GetByIdAsync(int id) =>
            await _context.Estados
                .Include(e => e.Pais)
                .FirstOrDefaultAsync(e => e.COD_ESTADO == id);

        public async Task AddAsync(Estado estado)
        {
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Estado estado)
        {
            _context.Estados.Update(estado);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var estado = await GetByIdAsync(id);
            if (estado != null)
            {
                _context.Estados.Remove(estado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Estado>> GetByPaisIdAsync(int paisId) =>
            await _context.Estados
                .Include(e => e.Pais)
                .Where(e => e.COD_PAIS == paisId)
                .ToListAsync();
        
        private static readonly Dictionary<string, string> UfToNome = new()
        {
            { "AC", "Acre" },
            { "AL", "Alagoas" },
            { "AP", "Amapá" },
            { "AM", "Amazonas" },
            { "BA", "Bahia" },
            { "CE", "Ceará" },
            { "DF", "Distrito Federal" },
            { "ES", "Espírito Santo" },
            { "GO", "Goiás" },
            { "MA", "Maranhão" },
            { "MT", "Mato Grosso" },
            { "MS", "Mato Grosso do Sul" },
            { "MG", "Minas Gerais" },
            { "PA", "Pará" },
            { "PB", "Paraíba" },
            { "PR", "Paraná" },
            { "PE", "Pernambuco" },
            { "PI", "Piauí" },
            { "RJ", "Rio de Janeiro" },
            { "RN", "Rio Grande do Norte" },
            { "RS", "Rio Grande do Sul" },
            { "RO", "Rondônia" },
            { "RR", "Roraima" },
            { "SC", "Santa Catarina" },
            { "SP", "São Paulo" },
            { "SE", "Sergipe" },
            { "TO", "Tocantins" }
        };

        public async Task<Estado?> GetByUfAsync(string uf)
        {
            if (!UfToNome.TryGetValue(uf.ToUpper(), out var nomeEstado))
                return null;

            return await _context.Estados
                .Include(e => e.Pais)
                .FirstOrDefaultAsync(e => e.NOME_ESTADO == nomeEstado);
        }
    }
}