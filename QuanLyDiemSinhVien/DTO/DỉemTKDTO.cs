namespace asd123.DTO;

public class DiemTKDTO
{
    // Thêm nếu cần định danh cụ thể cho các mục điểm số
    public Guid? Id { get; set; } 

    public float? DiemChuyenCan { get; set; }
    public float? DiemBaiTap { get; set; }
    public float? DiemThucHanh { get; set; }
    public float? DiemKiemTraGiuaKi { get; set; }
    public float? DiemThi { get; set; }

    // Thêm nếu cần tính và trả về điểm tổng kết
    public float? DiemTongKet { get; set; }

    // Thêm nếu cần xác định mối quan hệ
    public Guid MaSinhVien { get; set; } 
    public Guid MaMonHoc { get; set; } 
}