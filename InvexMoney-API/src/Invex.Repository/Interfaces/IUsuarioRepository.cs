using Invex.Domain.Entidades;

public interface IUsuarioRepository
{
    Task<int> CriarUsuario(Usuario usuario);
    Task Atualizar(Usuario usuario);
    Task <Usuario?> Obter (int usuarioId);
    Task<Usuario?> ObterPorEmail(string email);
    Task<List<Usuario>> Listar ();
}