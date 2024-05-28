using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd123.Model
{
    public class Major : BaseSchema
    {
        [Unicode]
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Class> Classes { get; set; }
        public List<Subject> Subjects { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
