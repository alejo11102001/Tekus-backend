using Microsoft.AspNetCore.Mvc;
using Tekus.Application.DTOs;
using Tekus.Application.Services;

namespace Tekus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvidersController : ControllerBase
{
    private readonly IProviderAppService _service;
    public ProvidersController(IProviderAppService service) { _service = service; }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]int page = 1, [FromQuery]int pageSize = 10, [FromQuery]string q = "", [FromQuery]string orderBy = "name")
    {
        var res = await _service.ListAsync(page, pageSize, q, orderBy);
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _service.GetByIdAsync(id);
        if (p == null) return NotFound();
        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProviderDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateProviderDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}