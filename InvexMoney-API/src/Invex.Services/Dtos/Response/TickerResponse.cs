namespace Invex.Services.Dtos.Response;

public class TickerResponse
{
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public QuoteResponse? Quote { get; set; }
}