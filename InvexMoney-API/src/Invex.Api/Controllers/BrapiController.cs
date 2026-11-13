using Microsoft.AspNetCore.Mvc;
using Invex.Services.Brapi;
using Invex.Services.Dtos.Response;
using Invex.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Invex.Application.Dtos.Response;


namespace Invex.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]

public class BrapiController : ControllerBase
{
    private readonly IBrapiService _brapiService;

    public BrapiController(IBrapiService brapiService)
    {
        _brapiService = brapiService;
    }


    [HttpGet("ObterAtivos")]
    public async Task<IActionResult> ObterAtivos()
    {
        return Ok(await _brapiService.ObterAtivosAsync(300));
    }

    [HttpGet("DestaquesDoDia")]
    public async Task<ActionResult<DestaquesResponse>> ObterDestaquesDoDia()
    {
        var ativos = await _brapiService.ObterAtivosAsync(300);

        var ativosmapeados = ativos.Select(a => new AtivoResponse
        {
            Ticker = a.Symbol,
            Nome = a.Name,
            Setor = a.Sector,
            Preco = a.Quote != null ? (decimal)a.Quote.LastPrice.GetValueOrDefault(): 0,
            Variacao = a.Quote != null ? (decimal)a.Quote.ChangePercent.GetValueOrDefault() : 0
        }).ToList();

        var maioresAltas = ativosmapeados
            .OrderByDescending(a => a.Variacao)
            .Take(5)
            .ToList();

        var maioresQuedas = ativosmapeados
        .OrderBy(a => a.Variacao)
        .Take(5)
        .ToList();



        var resposta = new DestaquesResponse
        {
            DataReferencia = DateOnly.FromDateTime(DateTime.Now),
            MaioresAltas = maioresAltas,
            MaioresQuedas = maioresQuedas,
            AtivoSubindo = ativosmapeados.Count(a => a.Variacao > 0),
            AitvoCaindo = ativosmapeados.Count(a => a.Variacao < 0)
        };
        return Ok(resposta);
    }
}

