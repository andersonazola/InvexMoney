using Invex.Application.Dtos;

public interface IRelatorioApplication
{
   Task<RelatorioResponse> GerarRelatorio();
   Task<RelatorioResponse?> ObterUltimoRelatorio();
}