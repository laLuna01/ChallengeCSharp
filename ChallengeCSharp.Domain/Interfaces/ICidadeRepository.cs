using ChallengeCSharp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Domain.Interfaces
{
    public interface ICidadeRepository
    {
        Task<IEnumerable<Cidade>> GetAllAsync();

        Task<Cidade?> GetByIdAsync(int id);

        Task AddAsync(Cidade cidade);

        Task UpdateAsync(Cidade cidade);

        Task DeleteAsync(int id);
        
        Task<Cidade?> GetByNomeAsync(string nome, int codEstado);
    }
}