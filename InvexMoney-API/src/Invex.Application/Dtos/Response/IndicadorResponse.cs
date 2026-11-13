namespace Invex.Application.Dtos.Response;

public class IndicadorResponse
{
    public string Nome {get; set;}  = string.Empty;
    public decimal Valor {get; set;}
    public decimal Variacao {get; set;}
    public string Unidade {get; set;} = string.Empty;
}

