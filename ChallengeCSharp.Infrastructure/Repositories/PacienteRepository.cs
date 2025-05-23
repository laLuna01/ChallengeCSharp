using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> GetAllAsync() =>
            await _context.Pacientes
                .Include(e => e.Genero) 
                .Include(e => e.Endereco)
                .ToListAsync();

        public async Task<Paciente?> GetByIdAsync(int id) =>
            await _context.Pacientes
                .Include(e => e.Genero) 
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(e => e.ID_PACIENTE == id);

        public async Task AddAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var paciente = await GetByIdAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Paciente>> GetByGeneroIdAsync(int generoId) =>
            await _context.Pacientes
                .Include(e => e.Endereco)
                .Include(e => e.Genero)
                .Where(e => e.GENERO_ID_GENERO == generoId)
                .ToListAsync();
    }
}