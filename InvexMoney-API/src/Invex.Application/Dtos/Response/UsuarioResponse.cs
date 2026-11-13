
using Invex.Domain.Entidades;

namespace Invex.Application.Dtos.Response;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
}