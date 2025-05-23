using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class DentistaService
{
    private readonly IDentistaRepository _dentistaRepository;
    private readonly IGeneroRepository _generoRepository;
    private readonly IEnderecoRepository _enderecoRepository;

    public DentistaService(IDentistaRepository dentistaRepository, IGeneroRepository generoRepository, IEnderecoRepository enderecoRepository)
    {
        _dentistaRepository = dentistaRepository;
        _generoRepository = generoRepository;
        _enderecoRepository = enderecoRepository;
    }

    public async Task<IEnumerable<Dentista>> GetAllAsync() => await _dentistaRepository.GetAllAsync();

    public async Task<Dentista?> GetByIdAsync(int id) => await _dentistaRepository.GetByIdAsync(id);

    public async Task AddAsync(Dentista dentista) => await _dentistaRepository.AddAsync(dentista);

    public async Task UpdateAsync(Dentista dentista) => await _dentistaRepository.UpdateAsync(dentista);

    public async Task DeleteAsync(int id) => await _dentistaRepository.DeleteAsync(id);

    public async Task<IEnumerable<Genero>> GetAllGenerosAsync() => await _generoRepository.GetAllAsync();
    
    public async Task<IEnumerable<Endereco>> GetAllEnderecosAsync() => await _enderecoRepository.GetAllAsync();
}