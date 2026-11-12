using Microsoft.AspNetCore.Mvc;
using Invex.Services.Brapi;
using Invex.Services.Dtos.Response;
using Invex.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


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
    public async Task<IActionResult>ObterAtivos ()
    {
        return Ok(await _brapiService.ObterAtivosAsync(300));
    }
}

