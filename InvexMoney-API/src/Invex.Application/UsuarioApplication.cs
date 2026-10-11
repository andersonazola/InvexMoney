using Invex.Application.Dtos.Request;
using Invex.Application.Dtos.Response;
using Invex.Domain.Entidades;
using Invex.Application.Interfaces;

namespace Invex.Application;

public class UsuarioApplication : IUsuarioApplication
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ISenhaHash _senhaHash;
    public UsuarioApplication(IUsuarioRepository usuarioRepository, ISenhaHash senhaHash)
    {
        _usuarioRepository = usuarioRepository;
        _senhaHash = senhaHash;
    }

    public async Task<UsuarioResponse> CadastrarUsuario(UsuarioCadastroRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        ValidarInformacoesCadastro(request);

        var email = request.Email.Trim().ToLowerInvariant();

        if (await _usuarioRepository.ObterPorEmail(email) != null)
            throw new InvalidOperationException("Email já cadastrado");


        var senhaHash = _senhaHash.GerarHash(request.Senha);

        var usuario = new Usuario(request.Nome, email, senhaHash);
        var id = await _usuarioRepository.CriarUsuario(usuario);


        return new UsuarioResponse()
        {
            Id = id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };

    }



    public async Task<LoginResponse> LoginUsuario(LoginRequest loginRequest)
    {
        if (loginRequest == null)
            throw new ArgumentNullException(nameof(loginRequest));

        ValidarInformacoesLogin(loginRequest);

        var email = loginRequest.Email.Trim().ToLowerInvariant();

        var usuario = await _usuarioRepository.ObterPorEmail(email);


        if (usuario == null || !_senhaHash.Verificar(loginRequest.Senha, usuario.Senha))
            throw new Exception("Email ou senha inválidos");



        return new LoginResponse()
        {
            Nome = usuario.Nome,
            Email = usuario.Email
        };

    }



    #region  Uteis
    private static void ValidarInformacoesCadastro(UsuarioCadastroRequest usuarioRequest)
    {
        //cadastro
        if (string.IsNullOrWhiteSpace(usuarioRequest.Nome))
            throw new ArgumentException("Nome não pode ser vazio");

        if (string.IsNullOrWhiteSpace(usuarioRequest.Email))
            throw new ArgumentException("Email não pode ser vazio");
        if (string.IsNullOrWhiteSpace(usuarioRequest.Senha))
            throw new ArgumentException("Senha não pode ser vazia");

        if (usuarioRequest.Senha.Length < 8)
            throw new ArgumentException("Senha deve ter no mínimo 8 caracteres");
        if (usuarioRequest.Senha.Length > 50)
            throw new ArgumentException("Senha deve ter no máximo 50 caracteres ");
    }

    private static void ValidarInformacoesLogin(LoginRequest loginRequest)
    {
        //Login
        if (string.IsNullOrWhiteSpace(loginRequest.Email))
            throw new ArgumentException("Email não pode ser vazio");
        if (string.IsNullOrWhiteSpace(loginRequest.Senha))
            throw new ArgumentException("Senha não pode ser vazia");
    }

    #endregion
}
