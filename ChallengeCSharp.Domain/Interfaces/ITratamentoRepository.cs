using ChallengeCSharp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Domain.Interfaces
{
    public interface ITratamentoRepository
    {
        Task<IEnumerable<Tratamento>> GetAllAsync();

        Task<Tratamento?> GetByIdAsync(int id);

        Task AddAsync(Tratamento tratamento);

        Task UpdateAsync(Tratamento tratamento);

        Task DeleteAsync(int id);
    }
}