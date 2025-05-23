using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class EstadoService
{
    private readonly IEstadoRepository _estadoRepository;
    private readonly IPaisRepository _paisRepository;

    public EstadoService(IEstadoRepository estadoRepository, IPaisRepository paisRepository)
    {
        _estadoRepository = estadoRepository;
        _paisRepository = paisRepository;
    }

    public async Task<IEnumerable<Estado>> GetAllAsync() => await _estadoRepository.GetAllAsync();

    public async Task<Estado?> GetByIdAsync(int id) => await _estadoRepository.GetByIdAsync(id);

    public async Task AddAsync(Estado estado) => await _estadoRepository.AddAsync(estado);

    public async Task UpdateAsync(Estado estado) => await _estadoRepository.UpdateAsync(estado);

    public async Task DeleteAsync(int id) => await _estadoRepository.DeleteAsync(id);

    public async Task<IEnumerable<Pais>> GetAllPaisesAsync() => await _paisRepository.GetAllAsync();
}