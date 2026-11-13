using Invex.Application.Interfaces;
using Invex.Application.Dtos.Response;
using Invex.Application.Dtos;
using Invex.Services.Interfaces;
using System.Text.Json;
using Invex.Domain.Entidades;
namespace Invex.Application;

public class RelatorioApplication : IRelatorioApplication
{
    private readonly IRelatorioReposirory _relatorioRepository;
    private readonly IBrapiService _brapiService;
    private readonly IAIService _aIService;
    private readonly TimeProvider _timeProvider;
    public RelatorioApplication(IRelatorioReposirory relatorioRepository, IBrapiService brapiService, IAIService aIService, TimeProvider timeProvider)
    {
        _relatorioRepository = relatorioRepository;
        _brapiService = brapiService;
        _aIService = aIService;
        _timeProvider = timeProvider;
    }

    public async Task<RelatorioResponse> GerarRelatorio()
    {
        TimeSpan IntervaloCache = TimeSpan.FromMinutes(5);
        var horaAtual = _timeProvider.GetUtcNow().UtcDateTime;
        var ultimoRelatorio = await _relatorioRepository.ObterUltimoRelatorio();

        if (ultimoRelatorio != null && (horaAtual - ultimoRelatorio.GeradoEm) < IntervaloCache)
        {
            return new RelatorioResponse
            {
                DataReferencia = ultimoRelatorio.DataReferencia,
                GeradoEm = ultimoRelatorio.GeradoEm,
                Conteudo = ultimoRelatorio.Conteudo,
                Reaproveitado = true
            };
        }

        var ativos = await _brapiService.ObterAtivosAsync(100);
        var resumoAtivos = JsonSerializer.Serialize(ativos.Take(10));

        var prompt = $"Gere um relatório análitico resumido da situação do mercado da bolsa de valores com base nestes ativos principais: {resumoAtivos}. Dê destaque ao comportamento geral do mercado hoje.";
        var respostaIa = await _aIService.GetAiResponse(prompt);

        var novoRelatorio = new Relatorio
        {
            DataReferencia = horaAtual.Date,
            GeradoEm = horaAtual,
            Conteudo = respostaIa
        };

        await _relatorioRepository.SalvarRelatorio(novoRelatorio);

        return new RelatorioResponse
        {
            DataReferencia = novoRelatorio.DataReferencia,
            GeradoEm = novoRelatorio.GeradoEm,
            Conteudo = novoRelatorio.Conteudo,
            Reaproveitado = false
        };
    }

    public async Task<RelatorioResponse?> ObterUltimoRelatorio()
    {
        var ultimo = await _relatorioRepository.ObterUltimoRelatorio();
        if (ultimo == null) return null;

        return new RelatorioResponse
        {
            DataReferencia = ultimo.DataReferencia,
            GeradoEm = ultimo.GeradoEm,
            Conteudo = ultimo.Conteudo,
            Reaproveitado = true
        };
    }
}