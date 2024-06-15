using System.Numerics;
using asd123.DTO;
using asd123.Model;

namespace asd123.Services;

public interface IStudent : IBaseService<Students>
{
    Class GetCodeClass(string code);
    Students getCodeStudents(string code);
    IEnumerable<MarksDto> GetMarksForStudent(int id);

}

public class StudentService : BaseService<Students, ApplicationDbContext>, IStudent
{
    private readonly ApplicationDbContext _ctx;

    public StudentService(ApplicationDbContext context) : base(context)
    {
        _ctx = context;
    }

    public Class GetCodeClass(string code)
    {
        return _ctx.Classes.FirstOrDefault(c => c.Name == code);
    }

    public Students getCodeStudents(string code)
    {
        return _ctx.Students.FirstOrDefault(s => s.Code == code);
    }

    public IEnumerable<MarksDto> GetMarksForStudent(int id)
    {
        var result = _ctx.Marks.Where(m => m.StudentId == id)
            .Select(m => new MarksDto
            {
                StudentId = m.StudentId,
                StudentName = m.Student.Name,
                SubjectName = m.Subject.Name,
                Midterm = m.Midterm,
                FinalExam = m.Final_Exam,
                Attendance = m.Attendance
            }).ToList();
        return result;
    }
}