using Invex.Services.Dtos.Response;

namespace Invex.Services.Interfaces;

public interface IBrapiService
{
    Task<List<TickerResponse>> ObterAtivosAsync (int limite);

}