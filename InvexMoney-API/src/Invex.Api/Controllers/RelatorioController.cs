using Microsoft.AspNetCore.Mvc;
using Invex.Application.Interfaces;
using Invex.Application.Dtos;
using Microsoft.AspNetCore.Authorization;


namespace Invex.Api.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]

public class RelatorioController : ControllerBase
{
    private readonly IRelatorioApplication _relatorioApplication;

    public RelatorioController(IRelatorioApplication relatorioApplication)
    {
        _relatorioApplication = relatorioApplication;
    }

    [HttpGet("GerarRelatorio")]
    public async Task<ActionResult<RelatorioResponse>> GerarRelatorio()
    {
        var relatorio = await _relatorioApplication.GerarRelatorio();
        return Ok(relatorio);
    }

    [HttpGet("UltimoRelatorio")]
    public async Task<ActionResult<RelatorioResponse>> ObterUltimoRelatorio()
    {
        var relatorio = await _relatorioApplication.ObterUltimoRelatorio();
        if(relatorio == null)
            return NotFound(new { mensgaem = "Nenhum relatório encontrado no banco de dados"});
        
        return Ok(relatorio);
    }



}