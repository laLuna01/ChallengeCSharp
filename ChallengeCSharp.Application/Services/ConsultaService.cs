using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class ConsultaService
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IDentistaRepository _dentistaRepository;

    public ConsultaService(IConsultaRepository consultaRepository, IPacienteRepository pacienteRepository, IDentistaRepository dentistaRepository)
    {
        _consultaRepository = consultaRepository;
        _pacienteRepository = pacienteRepository;
        _dentistaRepository = dentistaRepository;
    }

    public async Task<IEnumerable<Consulta>> GetAllAsync() => await _consultaRepository.GetAllAsync();

    public async Task<Consulta?> GetByIdAsync(int id) => await _consultaRepository.GetByIdAsync(id);

    public async Task AddAsync(Consulta consulta) => await _consultaRepository.AddAsync(consulta);

    public async Task UpdateAsync(Consulta consulta) => await _consultaRepository.UpdateAsync(consulta);

    public async Task DeleteAsync(int id) => await _consultaRepository.DeleteAsync(id);

    public async Task<IEnumerable<Paciente>> GetAllPacientesAsync() => await _pacienteRepository.GetAllAsync();
    
    public async Task<IEnumerable<Dentista>> GetAllDentistasAsync() => await _dentistaRepository.GetAllAsync();
}