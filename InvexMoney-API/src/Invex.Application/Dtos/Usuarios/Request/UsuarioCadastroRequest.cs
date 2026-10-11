using Invex.Domain.Entidades;

namespace Invex.Application.Dtos.Request;

public class UsuarioCadastroRequest
{
    public string Nome { get; set; }
    public string Email { get; set; }  
    public string Senha { get; set; }

}