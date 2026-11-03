using Invex.Domain.Entidades;

public interface IRelatorioReposirory
{
    Task SalvarRelatorio(Relatorio relatorio);
    Task EditarRelatorio(Relatorio relatorio);
}

