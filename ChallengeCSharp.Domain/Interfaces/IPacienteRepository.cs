using ChallengeCSharp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> GetAllAsync();

        Task<Paciente?> GetByIdAsync(int id);

        Task AddAsync(Paciente paciente);

        Task UpdateAsync(Paciente paciente);

        Task DeleteAsync(int id);
    }
}