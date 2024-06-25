// using asd123.DTO;
// using asd123.Services;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using asd123.Biz.Roles;
// using Microsoft.AspNetCore.Authorization;
//
// namespace asd123.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     [Authorize]
//     public class ThongKeController : ControllerBase
//     {
//         private readonly IThongKeService _thongKeService;
//         private readonly ILogger<ThongKeController> _logger;
//
//         public ThongKeController(IThongKeService thongKeService, ILogger<ThongKeController> logger)
//         {
//             _thongKeService = thongKeService;
//             _logger = logger;
//         }
//
//         // POST: api/ThongKe/ThongKeSinhVien
//         [HttpPost("ThongKeSinhVien")]
//         public async Task<ActionResult<IEnumerable<ThongKeSinhVienResponse>>> ThongKeSinhVien([FromBody] ThongKeRequest? request)
//         {
//             // if (request == null)
//             // {
//             //     _logger.LogWarning("ThongKeRequest data is null");
//             //     return BadRequest("ThongKeRequest data is null");
//             // }
//
//             try
//             {
//                 var response = await _thongKeService.ThongKeSinhVienAsync(request);
//                 _logger.LogInformation("ThongKeSinhVien successful");
//                 return Ok(response);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error occurred while processing ThongKeSinhVien");
//                 return StatusCode(500, $"Internal server error: {ex.Message}");
//             }
//         }
//
//         // // POST: api/ThongKe/BaoCaoDiem
//         // [HttpPost("BaoCaoDiem")]
//         // public async Task<ActionResult<IEnumerable<BaoCaoDiemResponse>>> BaoCaoDiem([FromBody] BaoCaoDiemRequest request)
//         // {
//         //     if (request == null)
//         //     {
//         //         _logger.LogWarning("BaoCaoDiemRequest data is null");
//         //         return BadRequest("BaoCaoDiemRequest data is null");
//         //     }
//         //
//         //     try
//         //     {
//         //         var response = await _thongKeService.BaoCaoDiemAsync(request);
//         //         _logger.LogInformation("BaoCaoDiem successful");
//         //         return Ok(response);
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         _logger.LogError(ex, "Error occurred while processing BaoCaoDiem");
//         //         return StatusCode(500, $"Internal server error: {ex.Message}");
//         //     }
//         // }
//
//         // // POST: api/ThongKe/BaoCaoThanhTich
//         // [HttpPost("BaoCaoThanhTich")]
//         // public async Task<ActionResult<IEnumerable<SinhVienThanhTichResponse>>> BaoCaoThanhTich([FromBody] BaoCaoThanhTichRequest request)
//         // {
//         //     if (request == null)
//         //     {
//         //         _logger.LogWarning("BaoCaoThanhTichRequest data is null");
//         //         return BadRequest("BaoCaoThanhTichRequest data is null");
//         //     }
//         //
//         //     try
//         //     {
//         //         var response = await _thongKeService.BaoCaoThanhTichAsync(request);
//         //         _logger.LogInformation("BaoCaoThanhTich successful");
//         //         return Ok(response);
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         _logger.LogError(ex, "Error occurred while processing BaoCaoThanhTich");
//         //         return StatusCode(500, $"Internal server error: {ex.Message}");
//         //     }
//         // }
//     }
// }
