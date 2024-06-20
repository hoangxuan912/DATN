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
public class DiemController : ControllerBase
{
    private readonly IDiem _diemService;
    private readonly ILogger<DiemController> _logger;

    public DiemController(IDiem diemService,ILogger<DiemController> logger)
    {
        _diemService = diemService;
        _logger = logger;
    }

    [HttpPost("NhapDiem")]
    public async Task<ActionResult<NhapDiemResponse>> NhapDiem(Guid sinhVienId, Guid monHocId, [FromBody] DiemDTO diemDTO)
    {
        _logger.LogInformation($"Starting Nhập Điểm for SinhVienId: {sinhVienId}, MonHocId: {monHocId}");

        if (diemDTO == null)
        {
            _logger.LogWarning("DiemDTO data is null");
            return BadRequest("DiemDTO data is null");
        }

        try
        {
            var response = await _diemService.NhapDiemAsync(sinhVienId, monHocId, diemDTO);
            if (response == null)
            {
                _logger.LogWarning($"Không thể nhập điểm for SinhVienId: {sinhVienId}, MonHocId: {monHocId}");
                return NotFound("Không thể nhập điểm.");
            }

            _logger.LogInformation("Nhập Điểm successfully completed.");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while Nhập Điểm.");
            return StatusCode(500, "Internal Server Error");
        }
    }


    [HttpPut("UpdateDiem")]
    public async Task<IActionResult> UpdateDiem(Guid sinhVienId, Guid monHocId, [FromBody] DiemDTO diemDTO)
    {
        _logger.LogInformation($"Starting Update Điểm for SinhVienId: {sinhVienId}, MonHocId: {monHocId}");

        if (diemDTO == null)
        {
            _logger.LogWarning("DiemDTO data is null");
            return BadRequest("DiemDTO data is null");
        }
    
        try
        {
            await _diemService.UpdateDiemAsync(sinhVienId, monHocId, diemDTO);
            _logger.LogInformation("Update Điểm successfully completed.");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating Điểm.");
            return StatusCode(500, "Internal Server Error");
        }
    }


    [HttpDelete("DeleteDiem")]
    public async Task<IActionResult> DeleteDiem(Guid sinhVienId, Guid monHocId)
    {
        _logger.LogInformation($"Starting Delete Điểm for SinhVienId: {sinhVienId}, MonHocId: {monHocId}");

        try
        {
            await _diemService.DeleteDiemAsync(sinhVienId, monHocId);
            _logger.LogInformation("Delete Điểm successfully completed.");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting Điểm.");
            return StatusCode(500, "Internal Server Error");
        }
    }


    [HttpGet("GetAllDiemBySinhVienId/{sinhVienId}")]
    public async Task<ActionResult<IEnumerable<Diem>>> GetAllDiemBySinhVienId(Guid sinhVienId)
    {
        _logger.LogInformation($"Fetching all scores for student with ID: {sinhVienId}");

        var diems = await _diemService.GetAllDiemBySinhVienIdAsync(sinhVienId);
        if (diems == null || !diems.Any())
        {
            _logger.LogWarning($"No scores found for student with ID: {sinhVienId}");
            return NotFound();
        }

        _logger.LogInformation($"Returning {diems.Count()} scores for student with ID: {sinhVienId}");
        return Ok(diems);
    }
    [HttpGet("GetAllDiemByMonHocId/{monHocId}")]
    public async Task<ActionResult<IEnumerable<Diem>>> GetAllDiemByMonHocId(Guid monHocId)
    {
        _logger.LogInformation($"Fetching all scores for course with ID: {monHocId}");

        var diems = await _diemService.GetAllDiemByMonHocIdAsync(monHocId);
        if (diems == null || !diems.Any())
        {
            _logger.LogWarning($"No scores found for course with ID: {monHocId}");
            return NotFound();
        }

        _logger.LogInformation($"Returning {diems.Count()} scores for course with ID: {monHocId}");
        return Ok(diems);
    }
    [HttpGet("TinhDiemTB/{sinhVienId}")]
    public async Task<ActionResult<TinhDiemTBResponse>> TinhDiemTB(Guid sinhVienId)
    {
        _logger.LogInformation($"Calculating average score for student with ID: {sinhVienId}");

        var response = await _diemService.TinhDiemTBAsync(sinhVienId);
        if (response == null)
        {
            _logger.LogWarning($"No data found when calculating average score for student with ID: {sinhVienId}");
            return NotFound();
        }

        _logger.LogInformation($"Average score calculated successfully for student with ID: {sinhVienId}");
        return Ok(response);
    }

}