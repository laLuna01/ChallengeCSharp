using System.Net.Http.Json;
using ChallengeCSharp.Application.DTOs;

namespace ChallengeCSharp.Application.Services.Integrations;

public class ViaCepService
{
    private readonly HttpClient _httpClient;

    public ViaCepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ViaCepResponse?> GetEnderecoByCepAsync(string cep)
    {
        var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        if (!response.IsSuccessStatusCode)
            return null;

        var endereco = await response.Content.ReadFromJsonAsync<ViaCepResponse>();
        return endereco;
    }
}