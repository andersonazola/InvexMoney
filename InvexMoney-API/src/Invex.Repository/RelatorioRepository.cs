using System.Data;
using System.Reflection;
using Dapper;
using Invex.Domain.Entidades;
using Invex.Repository.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class RelatorioRepository : BaseRepository, IRelatorioReposirory
{

    public RelatorioRepository(InvexContext context) : base(context) { }


    public async Task SalvarRelatorio(Relatorio relatorio)
    {
        const string sql = "sp_CriarRelatorio";

        var parametros = new
        {
            DataReferencia = relatorio.DataReferencia,
            GeradoEm = relatorio.GeradoEm,
            Conteudo = relatorio.Conteudo
        };

        string stringConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringConexao);

        await connection.ExecuteAsync(sql, parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task EditarRelatorio(Relatorio relatorio)
    {
        const string sql = "sp_AtualizarRelatorio";

        var parametros = new
        {
            Id = relatorio.Id,
            Datareferencia = relatorio.DataReferencia,
            GeradoEm = relatorio.GeradoEm,
            Conteudo = relatorio.Conteudo
        };

        string stringConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringConexao);

        await connection.ExecuteAsync(sql, parametros, commandType: CommandType.StoredProcedure);
    }
}