using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace asd123.Model
{
    public class Students : BaseSchema
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateOnly Dob { get; set; }
        public string HomeTown {  get; set; }
        public int ContactNumber { get; set; }
        public ICollection<Marks> Marks { get; set; }
        public int ClassID { get; set; }
        public Class Class { get; set; }
    }
}
