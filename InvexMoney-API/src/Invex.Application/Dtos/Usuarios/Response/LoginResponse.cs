using Invex.Domain.Entidades;

namespace Invex.Application.Dtos.Response;

public class LoginResponse
{
    public string Token {get; set;}
    public string Email {get; set;}
    public string Nome {get; set;}
}