using System.ComponentModel.DataAnnotations;

namespace asd123.DTO;

public class SinhVienDTO
{
    public Guid Id { get; set; }
    public string HoTen { get; set; }
    public DateTime NgaySinh { get; set; }
    public string Email { get; set; }
    public string SoDienThoai { get; set; }
    public Guid MaLop { get; set; }
}
public class CreateSinhVienDTO
{
    [Required]
    [StringLength(255)]
    public string HoTen { get; set; }

    public DateTime NgaySinh { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    [StringLength(15)]
    public string SoDienThoai { get; set; }

    public Guid MaLop { get; set; }
}
public class UpdateSinhVienDTO
{
    [Required]
    [StringLength(255)]
    public string HoTen { get; set; }

    public DateTime NgaySinh { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    [StringLength(15)]
    public string SoDienThoai { get; set; }
    public Guid MaLop { get; set; }

    // Không cần MaLop nếu không cho phép cập nhật lớp thông qua API này
}