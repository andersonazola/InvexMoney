using System.Net.Http.Headers;
using System.Net.Http.Json;
using Invex.Services.Dtos.Response;
using Invex.Services.Interfaces;
using Microsoft.Extensions.Configuration;


namespace Invex.Services.Brapi;

public class BrapiService : IBrapiService
{

    private readonly HttpClient _client;

    public BrapiService(IConfiguration config)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://brapi.dev/"),
            Timeout = TimeSpan.FromSeconds(60)
        };

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config["Brapi:Token"]);
    }

    public async Task<List<TickerResponse>> ObterAtivosAsync(int limite)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<BrapiTickersResponse>($"/api/v2/tickers?type=stock&sortBy=marketCap&sortOrder=desc&limit={limite}");
            return response?.Results ?? new List<TickerResponse>();
        }
        catch (UnauthorizedAccessException)
        {
            throw new UnauthorizedAccessException("Você não tem permissão para executar esse método");
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha interna, {ex.Message}");
        }
           
    }

}



