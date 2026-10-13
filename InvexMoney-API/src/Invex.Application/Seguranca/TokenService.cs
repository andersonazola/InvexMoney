using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Invex.Application.Interfaces;
using Invex.Domain.Entidades;
using Microsoft.IdentityModel.Tokens;

namespace Invex.Application.Seguranca;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GerarToken(Usuario usuario)
    {
        if(usuario == null)
            throw new ArgumentNullException(nameof(usuario));
        
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Chave));

        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);


        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, usuario.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Emissor,
            audience: _jwtSettings.Audiencia,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiracaoEmMinutos),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}