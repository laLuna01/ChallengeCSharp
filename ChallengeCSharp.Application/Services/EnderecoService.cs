using ChallengeCSharp.Application.Services.Integrations;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Application.Services;

public class EnderecoService
{
    private readonly IEnderecoRepository _enderecoRepository;
    
    private readonly IPaisRepository _paisRepository;
    private readonly IEstadoRepository _estadoRepository;
    private readonly ICidadeRepository _cidadeRepository;
    private readonly IBairroRepository _bairroRepository;
    
    private readonly ViaCepService _viaCepService;
    
    private static readonly Dictionary<string, string> _ufToNomeEstado = new()
    {
        { "AC", "Acre" },
        { "AL", "Alagoas" },
        { "AP", "Amapá" },
        { "AM", "Amazonas" },
        { "BA", "Bahia" },
        { "CE", "Ceará" },
        { "DF", "Distrito Federal" },
        { "ES", "Espírito Santo" },
        { "GO", "Goiás" },
        { "MA", "Maranhão" },
        { "MT", "Mato Grosso" },
        { "MS", "Mato Grosso do Sul" },
        { "MG", "Minas Gerais" },
        { "PA", "Pará" },
        { "PB", "Paraíba" },
        { "PR", "Paraná" },
        { "PE", "Pernambuco" },
        { "PI", "Piauí" },
        { "RJ", "Rio de Janeiro" },
        { "RN", "Rio Grande do Norte" },
        { "RS", "Rio Grande do Sul" },
        { "RO", "Rondônia" },
        { "RR", "Roraima" },
        { "SC", "Santa Catarina" },
        { "SP", "São Paulo" },
        { "SE", "Sergipe" },
        { "TO", "Tocantins" }
    };


    public EnderecoService(IEnderecoRepository enderecoRepository, IBairroRepository bairroRepository, ViaCepService viaCepService, IPaisRepository paisRepository, IEstadoRepository estadoRepository, ICidadeRepository cidadeRepository)
    {
        _enderecoRepository = enderecoRepository;
        
        _paisRepository = paisRepository;
        _estadoRepository = estadoRepository;
        _cidadeRepository = cidadeRepository;
        _bairroRepository = bairroRepository;
        
        _viaCepService = viaCepService;
    }

    public async Task<IEnumerable<Endereco>> GetAllAsync() => await _enderecoRepository.GetAllAsync();

    public async Task<Endereco?> GetByIdAsync(int id) => await _enderecoRepository.GetByIdAsync(id);

    public async Task AddAsync(Endereco endereco) => await _enderecoRepository.AddAsync(endereco);

    public async Task UpdateAsync(Endereco endereco) => await _enderecoRepository.UpdateAsync(endereco);

    public async Task DeleteAsync(int id) => await _enderecoRepository.DeleteAsync(id);

    public async Task<IEnumerable<Bairro>> GetAllBairrosAsync() => await _bairroRepository.GetAllAsync();
    
    public async Task<Endereco?> ObterEnderecoPorCepAsync(string cep)
    {
        var viaCep = await _viaCepService.GetEnderecoByCepAsync(cep);
        if (viaCep == null || string.IsNullOrWhiteSpace(viaCep.Logradouro))
            return null;

        // Verifica/Cria País (fixo: Brasil)
        var pais = await _paisRepository.GetByNomeAsync("Brasil");
        if (pais == null)
        {
            await _paisRepository.AddAsync(new Pais { NOME = "Brasil" });
            pais = await _paisRepository.GetByNomeAsync("Brasil");
        }

        // Verifica/Cria Estado
        var estado = await _estadoRepository.GetByUfAsync(viaCep.Uf);
        if (estado == null)
        {
            if (pais != null)
                await _estadoRepository.AddAsync(new Estado
                {
                    NOME_ESTADO = _ufToNomeEstado.TryGetValue(viaCep.Uf, out var nomeEstado) ? nomeEstado : viaCep.Uf,
                    COD_PAIS = pais.COD_PAIS
                });

            estado = await _estadoRepository.GetByUfAsync(viaCep.Uf);
        }

        // Verifica/Cria Cidade
        if (estado != null)
        {
            var cidade = await _cidadeRepository.GetByNomeAsync(viaCep.Localidade, estado.COD_ESTADO);
            if (cidade == null)
            {
                await _cidadeRepository.AddAsync(new Cidade
                {
                    NOME = viaCep.Localidade,
                    COD_ESTADO = estado.COD_ESTADO
                });

                cidade = await _cidadeRepository.GetByNomeAsync(viaCep.Localidade, estado.COD_ESTADO);
            }


            // Verifica/Cria Bairro
            if (cidade != null)
            {
                var bairro = await _bairroRepository.GetByNomeAsync(viaCep.Bairro, cidade.COD_CIDADE);
                if (bairro == null)
                {
                    await _bairroRepository.AddAsync(new Bairro
                    {
                        NOME = viaCep.Bairro,
                        COD_CIDADE = cidade.COD_CIDADE
                    });
                    bairro = await _bairroRepository.GetByNomeAsync(viaCep.Bairro, cidade.COD_CIDADE);
                }

                // Monta o endereço
                if (bairro != null)
                {
                    var endereco = new Endereco
                    {
                        LOGRADOURO = viaCep.Logradouro,
                        CEP = int.Parse(viaCep.Cep.Replace("-", "")),
                        COD_BAIRRO = bairro.COD_BAIRRO,
                        REFERENCIA = string.Empty, // Pode vir de input
                        NUMERO = 0 // Pode vir de input
                    };

                    return endereco;
                }
            }
        }

        return null;
    }

}