using asd123.Model;

namespace asd123.Services;

public interface IStudent : IBaseService<Students>
{
    Class GetCodeClass(string code);
    Students getCodeStudents(string code);

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
}