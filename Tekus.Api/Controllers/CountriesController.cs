using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Services;

namespace Tekus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _svc;
    public CountriesController(ICountryService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool refresh = false)
    {
        var list = await _svc.GetAllAsync(refresh);
        return Ok(list.Select(x => new { x.IsoCode, x.Name }));
    }
}