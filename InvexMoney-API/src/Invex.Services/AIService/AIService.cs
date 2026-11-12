using System.Net.Http.Headers;
using System.Text;
using Invex.Services.Interfaces;
using Microsoft.Extensions.Configuration;

public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AIService(IConfiguration config)
    {
        _httpClient = new HttpClient();
        _config = config;
    }

    public Task<string> GetAiResponse(string prompt)
    {
        throw new NotImplementedException();
    }
}
