using ChallengeCSharp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeCSharp.Domain.Interfaces
{
    public interface IBairroRepository
    {
        Task<IEnumerable<Bairro>> GetAllAsync();

        Task<Bairro?> GetByIdAsync(int id);

        Task AddAsync(Bairro bairro);

        Task UpdateAsync(Bairro bairro);

        Task DeleteAsync(int id);
        
        Task<Bairro?> GetByNomeAsync(string nome, int codCidade);
    }
}