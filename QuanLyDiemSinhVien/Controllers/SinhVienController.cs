using asd123.Biz.Roles;
using asd123.DTO;
using asd123.Model;
using asd123.Services;
using Microsoft.AspNetCore.Authorization;

namespace asd123.Controllers;


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SinhVienController : ControllerBase
{
    private readonly ISinhVien _sinhVienService;
    private readonly ILogger<SinhVienController> _logger;

    public SinhVienController(ISinhVien sinhVienService, ILogger<SinhVienController> logger)
    {
        _sinhVienService = sinhVienService;
        _logger = logger;
    }

    // Lấy tất cả sinh viên
    [HttpGet]
    public async Task<ActionResult<List<SinhVienDTO>>> GetAll()
    {
        try
        {
            var sinhViens = await _sinhVienService.GetAllSinhVienAsync();
            // Mapping to DTOs, assuming you have MapToDTO method or using a library like AutoMapper
            return Ok(sinhViens.Select(sv => MapToDTO(sv)));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get all students: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // Lấy thông tin một sinh viên bằng ID
    [HttpGet("{id}")]
    public async Task<ActionResult<SinhVienDTO>> GetById(Guid id)
    {
        try
        {
            var sinhVien = await _sinhVienService.GetSinhVienByIdAsync(id);
            if (sinhVien == null)
            {
                _logger.LogWarning($"Không tìm thấy sinh viên với ID {id}");
                return NotFound($"Không tìm thấy sinh viên với ID {id}");
            }
            var sinhVienDTO = MapToDTO(sinhVien);
            return Ok(sinhVienDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Lỗi khi lấy thông tin sinh viên với ID {id}");
            return StatusCode(500, $"Có lỗi xảy ra trong quá trình lấy thông tin sinh viên với ID {id}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<SinhVienDTO>> Create(CreateSinhVienDTO createDto)
    {
        try
        {
            // Mapping từ CreateSinhVienDTO sang SinhVien entity
            var newSinhVien = new SinhVien
            {
                HoTen = createDto.HoTen,
                NgaySinh = createDto.NgaySinh,
                Email = createDto.Email,
                SoDienThoai = createDto.SoDienThoai,
                MaLop = createDto.MaLop
            };

            // Gọi service để thêm sinh viên mới vào cơ sở dữ liệu
            await _sinhVienService.CreateSinhVienAsync(newSinhVien);

            // Mapping từ SinhVien entity sang SinhVienDTO để trả về
            var sinhVienDTO = new SinhVienDTO
            {
                Id = newSinhVien.Id,
                HoTen = newSinhVien.HoTen,
                NgaySinh = newSinhVien.NgaySinh,
                Email = newSinhVien.Email,
                SoDienThoai = newSinhVien.SoDienThoai,
                MaLop = newSinhVien.MaLop
            };

            // Trả về response Http 201 với thông tin của sinh viên mới tạo
            return CreatedAtAction(nameof(GetById), new { id = newSinhVien.Id }, sinhVienDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to create student: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSinhVien(Guid id, [FromBody] UpdateSinhVienDTO updateDto)
    {

        var sinhVienToUpdate = await _sinhVienService.GetSinhVienByIdAsync(id);
        if (sinhVienToUpdate == null)
        {
            return NotFound("The SinhVien record couldn't be found.");
        }

        // Mapping từ DTO sang Entity, cập nhật các trường cần thiết
        sinhVienToUpdate.HoTen = updateDto.HoTen;
        sinhVienToUpdate.NgaySinh = updateDto.NgaySinh;
        sinhVienToUpdate.Email = updateDto.Email;
        sinhVienToUpdate.SoDienThoai = updateDto.SoDienThoai;
        sinhVienToUpdate.MaLop = updateDto.MaLop;

        await _sinhVienService.UpdateSinhVienAsync(sinhVienToUpdate);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinhVien(Guid id)
    {
        try
        {
            var sinhVienToDelete = await _sinhVienService.GetSinhVienByIdAsync(id);
            if (sinhVienToDelete == null)
            {
                _logger.LogInformation($"SinhVien with id {id} not found.");
                return NotFound("The SinhVien record couldn't be found.");
            }

            await _sinhVienService.DeleteSinhVienAsync(id);
            _logger.LogInformation($"SinhVien with id {id} has been deleted.");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete SinhVien with id {id}: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpGet("GetSinhVienByLopId/{lopId}")]
    public async Task<IActionResult> GetSinhVienByLopId(Guid lopId, [FromQuery] PaginationDTO pagination)
    {
        try
        {
            var sinhViens = await _sinhVienService.GetSinhVienByLopIdAsync(lopId, pagination);
            var sinhVienDTOs = sinhViens.Select(sv => MapEntityToDTO(sv));
           
            return Ok(sinhVienDTOs);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi lấy sinh viên theo Id lớp {LopId}", lopId);
            return StatusCode(500, "Có lỗi xảy ra khi thực hiện yêu cầu");
        }
    }

    [HttpGet("SearchSinhVien")]
    public async Task<IActionResult> SearchSinhVien([FromQuery] string searchText, [FromQuery] PaginationDTO pagination)
    {
        try
        {
            var sinhViens = await _sinhVienService.SearchSinhVienAsync(searchText, pagination);
            var sinhVienDTOs = sinhViens.Select(sv => MapEntityToDTO(sv));
            
            return Ok(sinhVienDTOs);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi tìm kiếm sinh viên với từ khóa : '{SearchText}'", searchText);
            return StatusCode(500, "Có lỗi xảy ra khi thực hiện yêu cầu");
        }
    }
    
    private SinhVienDTO MapEntityToDTO(SinhVien sv)
    {
        return new SinhVienDTO
        {
            Id = sv.Id,
            HoTen = sv.HoTen,
            NgaySinh = sv.NgaySinh,
            Email = sv.Email,
            SoDienThoai = sv.SoDienThoai,
            MaLop = sv.MaLop
        };
    }
    
    private SinhVienDTO MapToDTO(SinhVien sv)
    {
        return new SinhVienDTO
        {
            Id = sv.Id,
            HoTen = sv.HoTen,
            NgaySinh = sv.NgaySinh,
            Email = sv.Email,
            SoDienThoai = sv.SoDienThoai,
            MaLop = sv.MaLop
        };
    }
}