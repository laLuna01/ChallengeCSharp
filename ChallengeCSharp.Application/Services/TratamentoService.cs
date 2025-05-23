using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class TratamentoService
{
    private readonly ITratamentoRepository _tratamentoRepository;
    private readonly IConsultaRepository _consultaRepository;

    public TratamentoService(ITratamentoRepository tratamentoRepository, IConsultaRepository consultaRepository)
    {
        _tratamentoRepository = tratamentoRepository;
        _consultaRepository = consultaRepository;
    }

    public async Task<IEnumerable<Tratamento>> GetAllAsync() => await _tratamentoRepository.GetAllAsync();

    public async Task<Tratamento?> GetByIdAsync(int id) => await _tratamentoRepository.GetByIdAsync(id);

    public async Task AddAsync(Tratamento tratamento) => await _tratamentoRepository.AddAsync(tratamento);

    public async Task UpdateAsync(Tratamento tratamento) => await _tratamentoRepository.UpdateAsync(tratamento);

    public async Task DeleteAsync(int id) => await _tratamentoRepository.DeleteAsync(id);

    public async Task<IEnumerable<Consulta>> GetAllConsultasAsync() => await _consultaRepository.GetAllAsync();
}