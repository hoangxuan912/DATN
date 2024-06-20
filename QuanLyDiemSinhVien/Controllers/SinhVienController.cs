using asd123.Model;
using asd123.Services;

namespace asd123.Controllers;


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SinhVienController : ControllerBase
{
    private readonly ISinhVien _sinhVienService;

    public SinhVienController(ISinhVien sinhVienService)
    {
        _sinhVienService = sinhVienService;
    }

    // GET: api/SinhVien
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SinhVien>>> GetAllSinhVien()
    {
        return Ok(await _sinhVienService.GetAllSinhVienAsync());
    }

    // GET: api/SinhVien/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SinhVien>> GetSinhVienById(Guid id)
    {
        var sinhVien = await _sinhVienService.GetSinhVienByIdAsync(id);
        if (sinhVien == null)
        {
            return NotFound();
        }
        return Ok(sinhVien);
    }

    // POST: api/SinhVien
    [HttpPost]
    public async Task<ActionResult<SinhVien>> CreateSinhVien([FromBody] SinhVien sinhVien)
    {
        if (sinhVien == null)
        {
            return BadRequest("SinhVien data is null");
        }

        var createdSinhVien = await _sinhVienService.CreateSinhVienAsync(sinhVien);
        return CreatedAtAction(nameof(GetSinhVienById), new { id = createdSinhVien.Id }, createdSinhVien);
    }

    // PUT: api/SinhVien/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSinhVien(Guid id, [FromBody] SinhVien sinhVien)
    {
        if (id != sinhVien.Id)
        {
            return BadRequest("SinhVien ID mismatch");
        }

        var sinhVienToUpdate = await _sinhVienService.GetSinhVienByIdAsync(id);
        if (sinhVienToUpdate == null)
        {
            return NotFound("The SinhVien record couldn't be found.");
        }

        await _sinhVienService.UpdateSinhVienAsync(sinhVien);
        return NoContent();
    }

    // DELETE: api/SinhVien/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinhVien(Guid id)
    {
        var sinhVienToDelete = await _sinhVienService.GetSinhVienByIdAsync(id);
        if (sinhVienToDelete == null)
        {
            return NotFound("The SinhVien record couldn't be found.");
        }

        await _sinhVienService.DeleteSinhVienAsync(id);
        return NoContent();
    }

    // GET: api/SinhVien/GetSinhVienByLopId/{lopId}
    [HttpGet("GetSinhVienByLopId/{lopId}")]
    public async Task<ActionResult<IEnumerable<SinhVien>>> GetSinhVienByLopId(Guid lopId)
    {
        return Ok(await _sinhVienService.GetSinhVienByLopIdAsync(lopId));
    }

    // GET: api/SinhVien/SearchSinhVien/{searchText}
    [HttpGet("SearchSinhVien/{searchText}")]
    public async Task<ActionResult<IEnumerable<SinhVien>>> SearchSinhVien(string searchText)
    {
        return Ok(await _sinhVienService.SearchSinhVienAsync(searchText));
    }
}