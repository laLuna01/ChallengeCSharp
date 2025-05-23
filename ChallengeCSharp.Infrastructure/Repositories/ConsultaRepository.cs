using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Infrastructure.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsultaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> GetAllAsync() =>
            await _context.Consultas
                .Include(e => e.Paciente) 
                .Include(e => e.Dentista)
                .ToListAsync();

        public async Task<Consulta?> GetByIdAsync(int id) =>
            await _context.Consultas
                .Include(e => e.Paciente) 
                .Include(e => e.Dentista)
                .FirstOrDefaultAsync(e => e.ID_CONSULTA == id);

        public async Task AddAsync(Consulta consulta)
        {
            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Consulta consulta)
        {
            _context.Consultas.Update(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var consulta = await GetByIdAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Consulta>> GetByPacienteIdAsync(int pacienteId) =>
            await _context.Consultas
                .Include(e => e.Paciente)
                .Include(e => e.Dentista)
                .Where(e => e.PACIENTE_ID_PACIENTE == pacienteId)
                .ToListAsync();

        public async Task<IEnumerable<Consulta>> GetByDentistaIdAsync(int dentistaId) =>
            await _context.Consultas
                .Include(e => e.Paciente)
                .Include(e => e.Dentista)
                .Where(e => e.DENTISTA_ID_DENTISTA == dentistaId)
                .ToListAsync();
    }
}