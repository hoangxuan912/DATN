using asd123.Model;
using asd123.Services;

namespace asd123.Controllers;


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MonHocController : ControllerBase
{
    private readonly IMonHoc _monHocService;

    public MonHocController(IMonHoc monHocService)
    {
        _monHocService = monHocService;
    }

    // GET: api/MonHoc
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MonHoc>>> GetAllMonHoc()
    {
        return Ok(await _monHocService.GetAllMonHocAsync());
    }

    // GET: api/MonHoc/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MonHoc>> GetMonHocById(Guid id)
    {
        var monHoc = await _monHocService.GetMonHocByIdAsync(id);
        if (monHoc == null)
        {
            return NotFound();
        }
        return Ok(monHoc);
    }

    // POST: api/MonHoc
    [HttpPost]
    public async Task<ActionResult<MonHoc>> CreateMonHoc([FromBody] MonHoc monHoc)
    {
        if (monHoc == null)
        {
            return BadRequest("MonHoc data is null");
        }

        var createdMonHoc = await _monHocService.CreateMonHocAsync(monHoc);
        return CreatedAtAction(nameof(GetMonHocById), new { id = createdMonHoc.Id }, createdMonHoc);
    }

    // PUT: api/MonHoc/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMonHoc(Guid id, [FromBody] MonHoc monHoc)
    {
        if (id != monHoc.Id)
        {
            return BadRequest("MonHoc ID mismatch");
        }

        var existingMonHoc = await _monHocService.GetMonHocByIdAsync(id);
        if (existingMonHoc == null)
        {
            return NotFound("The MonHoc record couldn't be found.");
        }

        await _monHocService.UpdateMonHocAsync(monHoc);
        return NoContent();
    }

    // DELETE: api/MonHoc/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMonHoc(Guid id)
    {
        var monHocToDelete = await _monHocService.GetMonHocByIdAsync(id);
        if (monHocToDelete == null)
        {
            return NotFound("The MonHoc record couldn't be found.");
        }

        await _monHocService.DeleteMonHocAsync(id);
        return NoContent();
    }

    // GET: api/MonHoc/GetMonHocByChuyenNganhId/{chuyenNganhId}
    [HttpGet("GetMonHocByChuyenNganhId/{chuyenNganhId}")]
    public async Task<ActionResult<IEnumerable<MonHoc>>> GetMonHocByChuyenNganhId(Guid chuyenNganhId)
    {
        return Ok(await _monHocService.GetMonHocByChuyenNganhIdAsync(chuyenNganhId));
    }

    // GET: api/MonHoc/SearchMonHocByName/{name}
    [HttpGet("SearchMonHocByName/{name}")]
    public async Task<ActionResult<IEnumerable<MonHoc>>> SearchMonHocByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Search text is null or empty.");
        }
        return Ok(await _monHocService.SearchMonHocByNameAsync(name));
    }
}