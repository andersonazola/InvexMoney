namespace Invex.Services.Interfaces;

public interface IBancoCentralService
{
    Task<List<decimal>> ObterUltimosValores (int codigo, int quantidade);
}