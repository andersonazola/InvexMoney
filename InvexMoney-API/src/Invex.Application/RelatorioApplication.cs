using Invex.Application.Interfaces;
using Invex.Application.Dtos.Response;
using Invex.Application.Dtos;

namespace Invex.Application;

public class RelatorioApplication : IRelatorioApplication
{
    private readonly IRelatorioReposirory _relatorioRepository;
    private readonly TimeProvider _timeProvider;
    public RelatorioApplication(IRelatorioReposirory relatorioReposirory, TimeProvider timeProvider)
    {
        _relatorioRepository = relatorioReposirory;
        _timeProvider = timeProvider;
    }

    public Task<RelatorioResponse> GerarRelatorio()
    {
        throw new NotImplementedException();
    }

    public Task<RelatorioResponse> ObterUltimoRelatorio()
    {
        throw new NotImplementedException();
    }
}