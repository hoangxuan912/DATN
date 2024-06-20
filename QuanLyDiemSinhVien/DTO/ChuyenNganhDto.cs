namespace asd123.DTO;

public class ChuyenNganhDto
{
    public Guid Id { get; set; } // Giả định rằng BaseSchema có thuộc tính Id
    public string TenChuyenNganh { get; set; }
    public Guid MaKhoa { get; set; } // Chỉ bao gồm MaKhoa nếu bạn chỉ muốn truyền id của Khoa mà không cần toàn bộ thông tin Khoa
}
