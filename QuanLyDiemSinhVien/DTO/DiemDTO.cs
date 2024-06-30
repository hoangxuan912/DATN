namespace asd123.DTO;

public class NhapDiemRequest
{
    public Guid SinhVienId { get; set; } // ID của sinh viên
    public Guid monHocId { get; set; } 
    public List<DiemDTO> DiemDTOList { get; set; } // Danh sách điểm của sinh viên (có thể nhập nhiều điểm)
}

public class DiemDTO
{
    public Guid SinhVienId { get; set; }
    public float? DiemChuyenCan { get; set; }
    public float? DiemBaiTap { get; set; }
    public float? DiemThucHanh { get; set; }
    public float? DiemKiemTraGiuaKi { get; set; }
    public float? DiemThi { get; set; }
}