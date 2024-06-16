using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace asd123.Model
{
    public class Subject : BaseSchema
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int TotalCreadits { get; set; }
        public int MajorId { get; set; }
        public Major Major { get; set; }
        public Marks Marks { get; set; }
    }
}