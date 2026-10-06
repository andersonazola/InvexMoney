using Invex.Domain.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Invex.Repository.Context;

namespace Invex.Repository;

public class UsuarioRepository : BaseRepository, IUsuarioRepository
{
    public UsuarioRepository(InvexContext context) : base(context){}

    public async Task<int> CriarUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
      await  _context.SaveChangesAsync();
        return  usuario.Id;
    }

    public async Task Atualizar(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
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

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        const string sql = @"
        SELECT UsuarioId AS Id, Nome, Email, Senha, DataCriacao 
        FROM Usuarios
        WHERE Email = @Email";

        string stringDeConexao = _context.Database.GetDbConnection().ConnectionString;
        using var connection = new SqlConnection(stringDeConexao);
        return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email });
    }
}