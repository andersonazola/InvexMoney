using Invex.Application.Interfaces;

namespace Invex.Application;

public class BCryptSenhaHash : ISenhaHash
{
    public string GerarHash(string senha)
        => BCrypt.Net.BCrypt.HashPassword(senha); // Sorteia o salt, gera o hash e devolve o texto de 60 caracter que vai pra coluna no banco
    

    public bool Verificar(string senha, string hash)
        => BCrypt.Net.BCrypt.Verify(senha, hash); // Lê o salt de dentro do hash, refaz o calculo com a senha digitada e compara
}
