using asd123.Model;

namespace asd123.Services;

public interface IMark: IBaseService<Marks>
{
    IEnumerable<Marks> getAllMarkBySubjectId(int id);
    IEnumerable<Marks> getAllMarkByStudentId(int id);
}

public class MarkService : BaseService<Marks, ApplicationDbContext>, IMark
{
    private readonly ApplicationDbContext _ctx;

    public MarkService(ApplicationDbContext context) : base(context)
    {
        _ctx = context;
    }
    public IEnumerable<Marks> getAllMarkBySubjectId(int id)
    {
        var result = _ctx.Marks.Where(m => m.SubjectId == id).ToList();
        return result;
    }

    public IEnumerable<Marks> getAllMarkByStudentId(int id)
    {
        var result = _ctx.Marks.Where(m => m.StudentId == id).ToList();
        return result;
    }
}