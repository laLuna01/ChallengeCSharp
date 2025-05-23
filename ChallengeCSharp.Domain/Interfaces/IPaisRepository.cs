using ChallengeCSharp.Domain.Entities;

namespace ChallengeCSharp.Domain.Interfaces
{
    public interface IPaisRepository
    {
        Task<IEnumerable<Pais>> GetAllAsync();

        Task<Pais?> GetByIdAsync(int id);

        Task AddAsync(Pais pais);

        Task UpdateAsync(Pais pais);

        Task DeleteAsync(int id);
        
        Task<Pais?> GetByNomeAsync(string nome);
    }
}