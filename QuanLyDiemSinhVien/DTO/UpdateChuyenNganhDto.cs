using System.ComponentModel.DataAnnotations;

namespace asd123.DTO;

public class UpdateChuyenNganhDto
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string TenChuyenNganh { get; set; }

    public Guid MaKhoa { get; set; }
}
