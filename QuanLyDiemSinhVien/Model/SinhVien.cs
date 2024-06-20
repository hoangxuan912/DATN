using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace asd123.Model
{
    public class SinhVien : BaseSchema
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

        [ForeignKey("MaLop")]
        public Lop Lop { get; set; }

        public ICollection<Diem> Diems { get; set; }
    }

}
