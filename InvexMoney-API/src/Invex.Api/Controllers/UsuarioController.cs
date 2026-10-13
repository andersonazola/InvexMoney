using Microsoft.AspNetCore.Mvc;
using Invex.Application.Dtos.Request;
using Invex.Application.Dtos.Response;
using Invex.Application;
using Invex.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Invex.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class UsuarioController : ControllerBase
{
    private readonly IUsuarioApplication _usuarioApplication;

    public UsuarioController(IUsuarioApplication usuarioApplication)
    {
        _usuarioApplication = usuarioApplication;
    }

    /// <summary>
    /// Cadastra um novo Usuário
    /// </summary>
    /// <param name="request"></param>
    [HttpPost]
    [Route("Cadastro")]
    public async Task<ActionResult<UsuarioResponse>> Cadastrar([FromBody] UsuarioCadastroRequest request)
    {
        var usuario = await _usuarioApplication.CadastrarUsuario(request);

        return StatusCode(StatusCodes.Status201Created, usuario);
    }


    /// <summary>
    /// Autentica o Usuário com email e senha
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>n 

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var loginResponse = await _usuarioApplication.LoginUsuario(loginRequest);

        return Ok(loginResponse);
    }


    /// <summary>
    /// Devolve os dados do usuário aunteticado 
    /// </summary>
    [Authorize]
    [HttpGet("Eu")]
    public IActionResult Eu()
    {
        var id = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

        return Ok(new { id, email });
    }

}