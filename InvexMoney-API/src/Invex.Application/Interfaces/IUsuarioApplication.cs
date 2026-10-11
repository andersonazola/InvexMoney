using System.Collections.Generic;
using System.Threading.Tasks;
using Invex.Application.Dtos.Request;
using Invex.Application.Dtos.Response;
using Invex.Domain.Entidades;

namespace Invex.Application.Interfaces;
 
public interface IUsuarioApplication
{
    Task<UsuarioResponse> CadastrarUsuario (UsuarioCadastroRequest request);
    Task<LoginResponse>LoginUsuario(LoginRequest loginRequest);

}