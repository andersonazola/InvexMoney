using Microsoft.AspNetCore.Mvc;
using Invex.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Invex.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]

public class BancoCentralController : ControllerBase
{
    private readonly IBancoCentralService _bancoCentral;

    public BancoCentralController(IBancoCentralService bancoCentral)
    {
        _bancoCentral = bancoCentral;
    }

    [HttpGet("ObterSelic")]
    public async Task<IActionResult> ObterSelic()
    {
        return Ok(await _bancoCentral.ObterUltimosValores(432, 1));
    }

    [HttpGet("ObterIpca")]
    public async Task<IActionResult> ObterIpca()
    {
        return Ok(await _bancoCentral.ObterUltimosValores(13522, 1));
    }

    [HttpGet("ObterPtax")]
    public async Task<IActionResult> ObterPtax()
    {
        return Ok(await _bancoCentral.ObterUltimosValores(1, 1));
    }
}
