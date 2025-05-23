using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class PacienteService
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IGeneroRepository _generoRepository;
    private readonly IEnderecoRepository _enderecoRepository;

    public PacienteService(IPacienteRepository pacienteRepository, IGeneroRepository generoRepository, IEnderecoRepository enderecoRepository)
    {
        _pacienteRepository = pacienteRepository;
        _generoRepository = generoRepository;
        _enderecoRepository = enderecoRepository;
    }

    public async Task<IEnumerable<Paciente>> GetAllAsync() => await _pacienteRepository.GetAllAsync();

    public async Task<Paciente?> GetByIdAsync(int id) => await _pacienteRepository.GetByIdAsync(id);

    public async Task AddAsync(Paciente paciente) => await _pacienteRepository.AddAsync(paciente);

    public async Task UpdateAsync(Paciente paciente) => await _pacienteRepository.UpdateAsync(paciente);

    public async Task DeleteAsync(int id) => await _pacienteRepository.DeleteAsync(id);

    public async Task<IEnumerable<Genero>> GetAllGenerosAsync() => await _generoRepository.GetAllAsync();
    
    public async Task<IEnumerable<Endereco>> GetAllEnderecosAsync() => await _enderecoRepository.GetAllAsync();
}