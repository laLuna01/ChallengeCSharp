using ChallengeCSharp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Domain.Interfaces
{
    public interface IEstadoRepository
    {
        Task<IEnumerable<Estado>> GetAllAsync();

        Task<Estado?> GetByIdAsync(int id);

        Task AddAsync(Estado estado);

        Task UpdateAsync(Estado estado);

        Task DeleteAsync(int id);
        
        Task<Estado?> GetByUfAsync(string uf);
    }
}