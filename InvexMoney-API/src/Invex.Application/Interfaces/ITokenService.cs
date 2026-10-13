using Invex.Domain.Entidades;

namespace Invex.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(Usuario usuario);
}