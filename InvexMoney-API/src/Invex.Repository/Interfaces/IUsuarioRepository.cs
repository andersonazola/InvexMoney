using Invex.Domain.Entidades;

public interface IUsuarioRepository
{
    Task CriarUsuario(Usuario usuario);
    Task AtualizarEmail(int usuarioId, string novoEmail);
    Task AtualizarSenha(int usuarioId, string novaSenha);
    Task <Usuario?> Obter (int usuarioId);
    Task<Usuario?> ObterPorEmail(string email);
    Task<List<Usuario>> Listar ();
    Task DeletarUsuario (int usuarioId);
}