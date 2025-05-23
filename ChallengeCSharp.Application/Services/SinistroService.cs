using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class SinistroService
{
    private readonly ISinistroRepository _sinistroRepository;
    private readonly IConsultaRepository _consultaRepository;

    public SinistroService(ISinistroRepository sinistroRepository, IConsultaRepository consultaRepository)
    {
        _sinistroRepository = sinistroRepository;
        _consultaRepository = consultaRepository;
    }

    public async Task<IEnumerable<Sinistro>> GetAllAsync() => await _sinistroRepository.GetAllAsync();

    public async Task<Sinistro?> GetByIdAsync(int id) => await _sinistroRepository.GetByIdAsync(id);

    public async Task AddAsync(Sinistro sinistro) => await _sinistroRepository.AddAsync(sinistro);

    public async Task UpdateAsync(Sinistro sinistro) => await _sinistroRepository.UpdateAsync(sinistro);

    public async Task DeleteAsync(int id) => await _sinistroRepository.DeleteAsync(id);

    public async Task<IEnumerable<Consulta>> GetAllConsultasAsync() => await _consultaRepository.GetAllAsync();
}