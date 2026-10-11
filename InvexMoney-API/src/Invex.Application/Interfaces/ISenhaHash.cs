
namespace Invex.Application.Interfaces;

public interface ISenhaHash
{
    string GerarHash(string senha);
    bool Verificar(string senha, string hash);
}