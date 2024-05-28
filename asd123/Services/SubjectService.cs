using asd123.Model;

namespace asd123.Services
{
    public interface ISubject : IBaseService<Subject>
    {
        Subject GetCodeSubject(string code);
    }
    public class SubjectService : BaseService<Subject, ApplicationDbContext>, ISubject
    {
        public readonly ApplicationDbContext ApplicationDbContext;
        public SubjectService(ApplicationDbContext context) : base(context)
        {
            ApplicationDbContext = context;
        }

        public Subject GetCodeSubject(string code)
        {
            var result = ApplicationDbContext.Subjects.FirstOrDefault(d => d.Code == code);
            return result;
        }
    }
}
