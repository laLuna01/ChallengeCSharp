using ChallengeCSharp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Domain.Interfaces
{
    public interface ISinistroRepository
    {
        Task<IEnumerable<Sinistro>> GetAllAsync();

        Task<Sinistro?> GetByIdAsync(int id);

        Task AddAsync(Sinistro sinistro);

        Task UpdateAsync(Sinistro sinistro);

        Task DeleteAsync(int id);
    }
}