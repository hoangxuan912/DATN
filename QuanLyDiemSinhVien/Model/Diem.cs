using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd123.Model
{
    public class Diem : BaseSchema
    {
        public Guid MaSinhVien { get; set; }

        [ForeignKey("MaSinhVien")]
        public SinhVien SinhVien { get; set; }

        public Guid MaMonHoc { get; set; }

        [ForeignKey("MaMonHoc")]
        public MonHoc MonHoc { get; set; }

        public float? DiemChuyenCan { get; set; }
        public float? DiemBaiTap { get; set; }
        public float? DiemThucHanh { get; set; }
        public float? DiemKiemTraGiuaKi { get; set; }
        public float? DiemThi { get; set; }

        [NotMapped]
        public float DiemTongKet { get; set; }
    }

}