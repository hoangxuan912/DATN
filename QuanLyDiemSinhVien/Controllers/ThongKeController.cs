using asd123.DTO;
using asd123.Services;

namespace asd123.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ThongKeController : ControllerBase
{
    private readonly IThongKeService _thongKeService;

    public ThongKeController(IThongKeService thongKeService)
    {
        _thongKeService = thongKeService;
    }

    // POST: api/ThongKe/ThongKeSinhVien
    [HttpPost("ThongKeSinhVien")]
    public async Task<ActionResult<IEnumerable<ThongKeSinhVienResponse>>> ThongKeSinhVien([FromBody] ThongKeRequest request)
    {
        if (request == null)
        {
            return BadRequest("ThongKeRequest data is null");
        }

        var response = await _thongKeService.ThongKeSinhVienAsync(request);
        return Ok(response);
    }

    // POST: api/ThongKe/BaoCaoDiem
    [HttpPost("BaoCaoDiem")]
    public async Task<ActionResult<IEnumerable<BaoCaoDiemResponse>>> BaoCaoDiem([FromBody] BaoCaoDiemRequest request)
    {
        if (request == null)
        {
            return BadRequest("BaoCaoDiemRequest data is null");
        }

        var response = await _thongKeService.BaoCaoDiemAsync(request);
        return Ok(response);
    }

    // POST: api/ThongKe/BaoCaoThanhTich
    [HttpPost("BaoCaoThanhTich")]
    public async Task<ActionResult<IEnumerable<SinhVienThanhTichResponse>>> BaoCaoThanhTich([FromBody] BaoCaoThanhTichRequest request)
    {
        if (request == null)
        {
            return BadRequest("BaoCaoThanhTichRequest data is null");
        }

        var response = await _thongKeService.BaoCaoThanhTichAsync(request);
        return Ok(response);
    }
}
