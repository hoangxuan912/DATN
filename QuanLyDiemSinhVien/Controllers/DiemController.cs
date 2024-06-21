using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd123.DTO;
using asd123.Model;
using asd123.Services;
using CsvHelper;
using CsvHelper.Configuration;
using OfficeOpenXml;

namespace asd123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiemController : ControllerBase
    {
        private readonly IDiem _diemService;
        private readonly IEmailservice _emailService;
        private readonly ILogger<DiemController> _logger;

        public DiemController(IDiem diemService, IEmailservice emailService, ILogger<DiemController> logger)
        {
            _diemService = diemService;
            _emailService = emailService;
            _logger = logger;
        }

        public DiemController(IDiem diemService, ILogger<DiemController> logger)
        {
            _diemService = diemService;
            _logger = logger;
        }

        // POST: api/Diem/NhapDiem
        [HttpPost("NhapDiem")]
        public async Task<ActionResult<NhapDiemResponse>> NhapDiem(Guid sinhVienId, Guid monHocId, [FromBody] DiemDTO diemDTO)
        {
            _logger.LogInformation($"Starting NhapDiem for SinhVienId: {sinhVienId}, MonHocId: {monHocId}");

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
                    _logger.LogWarning($"Khong the nhap diem for SinhVienId: {sinhVienId}, MonHocId: {monHocId}");
                    return NotFound("Khong the nhap diem.");
                }

                _logger.LogInformation("NhapDiem successfully completed.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while NhapDiem.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Diem/UpdateDiem
        [HttpPut("UpdateDiem")]
        public async Task<IActionResult> UpdateDiem(Guid sinhVienId, Guid monHocId, [FromBody] DiemDTO diemDTO)
        {
            _logger.LogInformation($"Starting UpdateDiem for SinhVienId: {sinhVienId}, MonHocId: {monHocId}");

            if (diemDTO == null)
            {
                _logger.LogWarning("DiemDTO data is null");
                return BadRequest("DiemDTO data is null");
            }

            try
            {
                await _diemService.UpdateDiemAsync(sinhVienId, monHocId, diemDTO);
                _logger.LogInformation("UpdateDiem successfully completed.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating Diem.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Diem/DeleteDiem
        [HttpDelete("DeleteDiem")]
        public async Task<IActionResult> DeleteDiem(Guid sinhVienId, Guid monHocId)
        {
            _logger.LogInformation($"Starting DeleteDiem for SinhVienId: {sinhVienId}, MonHocId: {monHocId}");

            try
            {
                await _diemService.DeleteDiemAsync(sinhVienId, monHocId);
                _logger.LogInformation("DeleteDiem successfully completed.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Diem.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/Diem/GetAllDiemBySinhVienId/{sinhVienId}
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

        // GET: api/Diem/GetAllDiemByMonHocId/{monHocId}
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

        // GET: api/Diem/TinhDiemTB/{sinhVienId}
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
        [HttpGet("SendDiemByEmail/{sinhVienId}")]
        public async Task<IActionResult> SendDiemByEmail(Guid sinhVienId)
        {
            _logger.LogInformation($"Sending score report by email for student with ID: {sinhVienId}");

            try
            {
                var diems = await _diemService.GetAllDiemBySinhVienIdAsync(sinhVienId);
                if (diems == null || !diems.Any())
                {
                    _logger.LogWarning($"No scores found for student with ID: {sinhVienId}");
                    return NotFound("No scores found for this student.");
                }

                var studentFullName = diems.FirstOrDefault()?.SinhVien?.HoTen;
                var emailContent = GenerateEmailContent(studentFullName, diems);

                // Send email using EmailService
                _emailService.SendEmail(emailContent);

                _logger.LogInformation($"Score report sent successfully by email to student with ID: {sinhVienId}");
                return Ok("Score report sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while sending score report by email for student with ID: {sinhVienId}");
                return StatusCode(500, "Error occurred while sending score report.");
            }
        }

        private Message GenerateEmailContent(string studentName, IEnumerable<Diem> diems)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Dear {studentName},<br/><br/>");
            sb.AppendLine("Your score report is as follows:<br/><br/>");

            foreach (var diem in diems)
            {
                sb.AppendLine($"Subject: {diem.MonHoc?.TenMonHoc}<br/>");
                sb.AppendLine($"Final Score: {diem.DiemTongKet}<br/><br/>");
            }

            return new Message(new string[] { "student-email@example.com" }, "Your Score Report", sb.ToString());
        }
        [HttpGet("ExportDiemToExcel/{monHocId}")]
        public async Task<IActionResult> ExportDiemToExcel(Guid monHocId)
        {
            _logger.LogInformation($"Exporting scores to Excel for subject with ID: {monHocId}");

            try
            {
                var diems = await _diemService.GetAllDiemByMonHocIdAsync(monHocId);
                if (diems == null || !diems.Any())
                {
                    _logger.LogWarning($"No scores found for subject with ID: {monHocId}");
                    return NotFound("No scores found for this subject.");
                }

                // Call method to create Excel file and return FileContentResult
                var fileBytes = await CreateExcelFile(diems);
                var fileName = $"Diem_MonHoc_{monHocId}.xlsx";

                _logger.LogInformation($"Score report exported to Excel successfully for subject with ID: {monHocId}");
        
                // Return the Excel file as FileContentResult
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while exporting scores to Excel for subject with ID: {monHocId}");
                return StatusCode(500, "Error occurred while exporting scores to Excel.");
            }
        }


        private async Task<byte[]> CreateExcelFile(IEnumerable<Diem> diems)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Scores");

            // Create header row
            worksheet.Cells["A1"].Value = "Student Name";
            worksheet.Cells["B1"].Value = "Subject";
            worksheet.Cells["C1"].Value = "Attendance Score";
            worksheet.Cells["D1"].Value = "Assignment Score";
            worksheet.Cells["E1"].Value = "Lab Score";
            worksheet.Cells["F1"].Value = "Midterm Score";
            worksheet.Cells["G1"].Value = "Final Exam Score";
            worksheet.Cells["H1"].Value = "Final Score";

            // Fill data rows
            int row = 2;
            foreach (var diem in diems)
            {
                worksheet.Cells[$"A{row}"].Value = diem.SinhVien?.HoTen;
                worksheet.Cells[$"B{row}"].Value = diem.MonHoc?.TenMonHoc;
                worksheet.Cells[$"C{row}"].Value = diem.DiemChuyenCan;
                worksheet.Cells[$"D{row}"].Value = diem.DiemBaiTap;
                worksheet.Cells[$"E{row}"].Value = diem.DiemThucHanh;
                worksheet.Cells[$"F{row}"].Value = diem.DiemKiemTraGiuaKi;
                worksheet.Cells[$"G{row}"].Value = diem.DiemThi;
                worksheet.Cells[$"H{row}"].Value = diem.DiemTongKet;

                row++;
            }

            // Auto fit columns
            worksheet.Cells.AutoFitColumns();

            // Convert ExcelPackage to byte array
            return await package.GetAsByteArrayAsync();
        }


    }
}
