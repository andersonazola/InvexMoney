using Invex.Domain.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Invex.Repository.Context;
using System.Data;
using System.Diagnostics;

namespace Invex.Repository;

public class UsuarioRepository : BaseRepository, IUsuarioRepository
{
    public UsuarioRepository(InvexContext context) : base(context) { }

    public async Task CriarUsuario(Usuario usuario)
    {
        const string sql = "sp_CadastrarUsuario";

        var parametros = new
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Senha = usuario.Senha,
            DataCriacao = usuario.DataCriacao
        };
        string stringConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringConexao);

        await connection.ExecuteAsync(sql, parametros, commandType: CommandType.StoredProcedure);
    }

    public async Task AtualizarEmail(int usuarioId, string novoEmail)
    {
        var parametros = new { UsuarioId = usuarioId, NovoEmail = novoEmail };

        string stringConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringConexao);
        await connection.ExecuteAsync("sp_AtualizarEmailUsuario", parametros, commandType: CommandType.StoredProcedure);
    }

    public async Task AtualizarSenha(int usuarioId, string novaSenha)
    {
        var parametros = new { UsuarioId = usuarioId, NovaSenha = novaSenha };

        string stringConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringConexao);
        await connection.ExecuteAsync("sp_AtualizaSenhaUsuario", parametros, commandType: CommandType.StoredProcedure);
    }


    public async Task<List<Usuario>> Listar()
    {
        const string sql = @"
        SELECT * 
        FROM Usuarios";

        string stringDeConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringDeConexao);
        var resultado = await connection.QueryAsync<Usuario>(sql);
        return resultado.ToList();
    }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        const string sql = "sp_ObterUsuarioPorEmail";

        var parametros = new { Email = email };

        string stringConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringConexao);

        return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, parametros, commandType: CommandType.StoredProcedure);
    }

    public async Task<Usuario?> Obter(int usuarioId)
    {
        const string sql = @"
        SELECT  UsuarioId AS Id, Nome, Email, Senha, DataCriacao 
        FROM Usuarios
        WHERE UsuarioId = @UsuarioId";

        string stringDeConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringDeConexao);
        return await connection.QuerySingleOrDefaultAsync<Usuario>(sql, new { UsuarioId = usuarioId });
    }


    public async Task DeletarUsuario(int UsuarioId)
    {
        const string sql = "sp_DeletarUsuario";
        var parametros = new { UsuarioId = UsuarioId };

        string stringConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringConexao);

        await connection.ExecuteAsync(sql, parametros, commandType: CommandType.StoredProcedure);
    }

}