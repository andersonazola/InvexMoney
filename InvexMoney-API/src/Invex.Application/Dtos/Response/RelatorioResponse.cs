using Invex.Domain.Entidades;

namespace Invex.Application.Dtos;

public class RelatorioResponse
{
    public DateTime DataReferencia {get; set;}
    public DateTime GeradoEm {get; set;}
    public string Conteudo {get; set;}
    public bool Reaproveitado {get; set;}
}