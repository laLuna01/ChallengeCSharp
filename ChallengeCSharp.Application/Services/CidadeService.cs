using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class CidadeService
{
    private readonly ICidadeRepository _cidadeRepository;
    private readonly IEstadoRepository _estadoRepository;

    public CidadeService(ICidadeRepository cidadeRepository, IEstadoRepository estadoRepository)
    {
        _cidadeRepository = cidadeRepository;
        _estadoRepository = estadoRepository;
    }

    public async Task<IEnumerable<Cidade>> GetAllAsync() => await _cidadeRepository.GetAllAsync();

    public async Task<Cidade?> GetByIdAsync(int id) => await _cidadeRepository.GetByIdAsync(id);

    public async Task AddAsync(Cidade cidade) => await _cidadeRepository.AddAsync(cidade);

    public async Task UpdateAsync(Cidade cidade) => await _cidadeRepository.UpdateAsync(cidade);

    public async Task DeleteAsync(int id) => await _cidadeRepository.DeleteAsync(id);

    public async Task<IEnumerable<Estado>> GetAllEstadosAsync() => await _estadoRepository.GetAllAsync();
}