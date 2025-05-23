using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class BairroService
{
    private readonly IBairroRepository _bairroRepository;
    private readonly ICidadeRepository _cidadeRepository;

    public BairroService(IBairroRepository bairroRepository, ICidadeRepository cidadeRepository)
    {
        _bairroRepository = bairroRepository;
        _cidadeRepository = cidadeRepository;
    }

    public async Task<IEnumerable<Bairro>> GetAllAsync() => await _bairroRepository.GetAllAsync();

    public async Task<Bairro?> GetByIdAsync(int id) => await _bairroRepository.GetByIdAsync(id);

    public async Task AddAsync(Bairro bairro) => await _bairroRepository.AddAsync(bairro);

    public async Task UpdateAsync(Bairro bairro) => await _bairroRepository.UpdateAsync(bairro);

    public async Task DeleteAsync(int id) => await _bairroRepository.DeleteAsync(id);

    public async Task<IEnumerable<Cidade>> GetAllCidadesAsync() => await _cidadeRepository.GetAllAsync();
}