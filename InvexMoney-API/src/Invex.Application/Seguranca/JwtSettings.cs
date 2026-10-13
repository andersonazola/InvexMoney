
namespace Invex.Application.Seguranca;

public class JwtSettings
{
    public const string Secao = "JwtSettings";

    public string Chave { get; set; } = string.Empty;
    public string Emissor { get; set; } = string.Empty;
    public string Audiencia { get; set; } = string.Empty;
    public int ExpiracaoEmMinutos { get; set; } = 60;
}