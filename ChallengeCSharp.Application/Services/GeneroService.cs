using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class GeneroService
{
    private readonly IGeneroRepository _repository;

    public GeneroService(IGeneroRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Genero>> GetAllAsync() => _repository.GetAllAsync();
    
    public Task<Genero?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    
    public Task AddAsync(Genero genero) => _repository.AddAsync(genero);
    
    public Task UpdateAsync(Genero genero) => _repository.UpdateAsync(genero);
    
    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
}