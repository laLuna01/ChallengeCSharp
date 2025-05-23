using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services
{
    public class PaisService
    {
        private readonly IPaisRepository _repository;

        public PaisService(IPaisRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Pais>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Pais?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task AddAsync(Pais pais) => _repository.AddAsync(pais);

        public Task UpdateAsync(Pais pais) => _repository.UpdateAsync(pais);

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}