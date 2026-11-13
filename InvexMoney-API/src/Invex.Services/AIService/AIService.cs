using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Invex.Services.Interfaces;
using Microsoft.Extensions.Configuration;

public class AIService : IAIService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;

    public AIService(IConfiguration config)
    {
        _client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };
        _config = config;
    }

    public async Task<string> GetAiResponse(string prompt)
    {
        var url = _config["Groq:ApiUrl"];
        var modelo = _config["Groq:Model"];
        var token = _config["Groq:Token"];

        if (string.IsNullOrEmpty(url))
            throw new InvalidOperationException("Configuração nula: Groq:ApiUrl");
        if (string.IsNullOrEmpty(modelo))
            throw new InvalidOperationException("Configuração nula: Groq:Model");
        if (string.IsNullOrEmpty(token))
            throw new InvalidOperationException("Configuração nula : Groq:Token");


        var req = new HttpRequestMessage(HttpMethod.Post, url);
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        req.Headers.UserAgent.ParseAdd("InvexMoneyApp");


        req.Content = JsonContent.Create(new
        {
            model = modelo,
            temperature = 0.3,
            max_tokens = 1000,
            messages = new[]
            {
                new {role = "user", content = prompt}
            }
        });

        using var resposta = await _client.SendAsync(req);
        var corpo = await resposta.Content.ReadAsStringAsync();

        if (!resposta.IsSuccessStatusCode)
            throw new HttpRequestException($"Erro na api da Groq: {corpo}");

        using var documento = JsonDocument.Parse(corpo);
        var texto = documento.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return texto ?? "";
    }




}
