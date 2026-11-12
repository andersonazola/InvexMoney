using Invex.Application.Interfaces;
using Invex.Application.Dtos.Response;
using Invex.Application.Dtos;

namespace Invex.Application;

public class RelatorioApplication : IRelatorioApplication
{
    private readonly IRelatorioReposirory _relatorioRepository;
    private readonly TimeProvider _timeProvider;
    public RelatorioApplication(IRelatorioReposirory relatorioRepository, TimeProvider timeProvider)
    {
        _relatorioRepository = relatorioRepository;
        _timeProvider = timeProvider;
    }

    public async Task<RelatorioResponse> GerarRelatorio()
    {
        TimeSpan Intervalo = TimeSpan.FromMinutes(15);
        var horaAtual = _timeProvider.GetUtcNow().UtcDateTime;
        var ultimo = await _relatorioRepository.ObterUltimoRelatorio();

        throw new NotImplementedException();

    }

    public Task<RelatorioResponse?> ObterUltimoRelatorio()
    {
        throw new NotImplementedException();
    }
}