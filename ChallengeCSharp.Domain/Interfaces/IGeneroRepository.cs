using ChallengeCSharp.Domain.Entities;

namespace ChallengeCSharp.Domain.Interfaces;

public interface IGeneroRepository
{
    Task<IEnumerable<Genero>> GetAllAsync();
    
    Task<Genero?> GetByIdAsync(int id);
    
    Task AddAsync(Genero genero);
    
    Task UpdateAsync(Genero genero);
    
    Task DeleteAsync(int id);
}