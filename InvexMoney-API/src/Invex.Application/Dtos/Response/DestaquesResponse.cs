namespace Invex.Application.Dtos.Response;

// O ranking do dia
public class DestaquesResponse
{
    public DateOnly DataReferencia  {get; set;}
    public List<AtivoResponse> MaioresAltas {get; set;} = new();
    public List<AtivoResponse> MaioresQuedas {get; set;} = new ();
    public int AtivoSubindo {get; set;}
    public int AitvoCaindo {get; set;}
} 

