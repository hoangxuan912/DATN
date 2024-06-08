using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace asd123.Model
{
    public class Class : BaseSchema
    {
        
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Students> Students { get; set; }
        public int MajorId { get; set; }
        public Major Major {  get; set; }
    }
}
