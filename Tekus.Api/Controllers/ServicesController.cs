using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Services;
using Tekus.Application.DTOs;

namespace Tekus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceAppService _svc;
    public ServicesController(IServiceAppService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]int page = 1, [FromQuery]int pageSize = 10, [FromQuery]string q = "", [FromQuery]string orderBy = "name")
    {
        var res = await _svc.ListAsync(page, pageSize, q, orderBy);
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var s = await _svc.GetByIdAsync(id);
        if (s == null) return NotFound();
        return Ok(s);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceDto dto)
    {
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateServiceDto dto)
    {
        await _svc.UpdateAsync(id, dto);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _svc.DeleteAsync(id);
        return NoContent();
    }
}