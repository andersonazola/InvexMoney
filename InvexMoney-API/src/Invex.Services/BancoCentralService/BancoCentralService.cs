using System.Globalization;
using System.Net.Http.Json;
using Invex.Services.Dtos.Response;
using Invex.Services.Interfaces;

namespace Invex.Services.BancoCentral;

public class BancoCentralService : IBancoCentralService
{
    private readonly HttpClient _client;

    public BancoCentralService(HttpClient client)
    {
 
        _client = client;
        _client.BaseAddress = new Uri("https://api.bcb.gov.br/"); 
        _client.Timeout = TimeSpan.FromSeconds(60);
    }
    public async Task<List<decimal>> ObterUltimosValores(int codigo, int quantidade)
    {
        try
        {
            var indices = await _client.GetFromJsonAsync<List<BancoCentralResponse>>($"dados/serie/bcdata.sgs.{codigo}/dados/ultimos/{quantidade}?formato=json");
            var valores = new List<decimal>();
            if (indices == null)
                return valores;

            foreach (var i in indices)
            {
                //Banco central mando com . como separado e o windows separa decimal com , 
                var valorFormatado = i.Valor.Replace('.', ',');
                valores.Add(decimal.Parse(valorFormatado));
            }
            return valores;
        }
        catch (UnauthorizedAccessException)
        {
            throw new UnauthorizedAccessException("Você não tem permissão para executar esse método");
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao executar método, {ex.Message}");
        }

    }
}
