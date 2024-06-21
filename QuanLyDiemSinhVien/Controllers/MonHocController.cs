using asd123.DTO;
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
    private readonly ILogger<MonHocController> _logger;

    public MonHocController(IMonHoc monHocService, ILogger<MonHocController> logger)
    {
        _monHocService = monHocService;
        _logger = logger;
    }

    // API lấy tất cả môn học
    [HttpGet]
    public async Task<IActionResult> GetAllMonHoc()
    {
        try
        {
            var monHocs = await _monHocService.GetAllMonHocAsync();
            // Chuyển đổi sang DTO (được giả định đã tạo) trước khi trả về
            return Ok(monHocs);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetAllMonHocs action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // API lấy một môn học theo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMonHocById(Guid id)
    {
        try
        {
            var monHoc = await _monHocService.GetMonHocByIdAsync(id);
            if (monHoc == null)
            {
                _logger.LogError($"MonHoc with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            // Chuyển đổi sang DTO (được giả định đã tạo) 
            return Ok(monHoc);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetMonHocById action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // API tạo mới một môn học
    [HttpPost]
    public async Task<IActionResult> CreateMonHoc([FromBody] MonHoc monHoc)
    {
        try
        {
            if (monHoc == null)
            {
                _logger.LogError("MonHoc object sent from client is null.");
                return BadRequest("MonHoc object is null");
            }

            await _monHocService.CreateMonHocAsync(monHoc);
            return CreatedAtRoute("MonHocById", new { id = monHoc.Id }, monHoc);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside CreateMonHoc action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // API cập nhật môn học
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMonHoc(Guid id, [FromBody] MonHoc monHoc)
    {
        try
        {
            if (monHoc == null)
            {
                _logger.LogError("MonHoc object sent from client is null.");
                return BadRequest("MonHoc object is null");
            }

            var dbMonHoc = await _monHocService.GetMonHocByIdAsync(id);
            if (dbMonHoc == null)
            {
                _logger.LogError($"MonHoc with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            await _monHocService.UpdateMonHocAsync(monHoc);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside UpdateMonHoc action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // API xóa môn học
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMonHoc(Guid id)
    {
        try
        {
            var monHoc = await _monHocService.GetMonHocByIdAsync(id);
            if (monHoc == null)
            {
                _logger.LogError($"MonHoc with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            await _monHocService.DeleteMonHocAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside DeleteMonHoc action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }


// GET: api/MonHoc/GetMonHocByChuyenNganhId/{chuyenNganhId}
    [HttpGet("GetMonHocByChuyenNganhId/{chuyenNganhId}")]
    public async Task<ActionResult<IEnumerable<MonHocDTO>>> GetMonHocByChuyenNganhId(Guid chuyenNganhId)
    {
        try
        {
            var monHocs = await _monHocService.GetMonHocByChuyenNganhIdAsync(chuyenNganhId);
            // Chuyển đổi monHocs sang MonHocDTO (giả sử đã có hàm chuyển đổi)
            return Ok(monHocs); // Sử dụng DTO thay vì entity trực tiếp
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetMonHocByChuyenNganhId action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

// GET: api/MonHoc/SearchMonHocByName/{name}
    [HttpGet("SearchMonHocByName/{name}")]
    public async Task<ActionResult<IEnumerable<MonHocDTO>>> SearchMonHocByName(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogWarning("SearchMonHocByName called with null or empty search text.");
                return BadRequest("Search text is null or empty.");
            }

            var monHocs = await _monHocService.SearchMonHocByNameAsync(name);
            // Chuyển đổi monHocs sang MonHocDTO (giả sử đã có hàm chuyển đổi)
            return Ok(monHocs); // Sử dụng DTO thay vì entity trực tiếp
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside SearchMonHocByName action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}