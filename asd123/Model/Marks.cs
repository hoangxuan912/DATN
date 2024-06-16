using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd123.Model
{
    public class Marks : BaseSchema
    {
        public int StudentId { get; set; }
        public Students Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public float Midterm { get; set; }
        public float Final_Exam { get; set; }
        public int Attendance { get; set; }
    }

}