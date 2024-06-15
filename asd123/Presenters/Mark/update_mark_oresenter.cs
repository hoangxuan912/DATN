using System.ComponentModel.DataAnnotations;

namespace asd123.Presenters.Mark;

public class update_mark_oresenter
{
    [Required] public int StudentId { get; set; }
    [Required] public int SubjectId { get; set; }
    [Required] public int Midterm { get; set; }
    [Required] public int Final_Exam { get; set; }
    [Required] public int Attendance { get; set; }
}