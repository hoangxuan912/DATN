using asd123.Model;

namespace asd123.Services
{
    public interface IClass : IBaseService<Class>
    {
        Class GetCodeClass(string code);
    }
    public class ClassService : BaseService<Class, ApplicationDbContext>, IClass
    {
        public readonly ApplicationDbContext ApplicationDbContext;
        public ClassService(ApplicationDbContext context) : base(context)
        {
            ApplicationDbContext = context;
        }
        public Class GetCodeClass(string code)
        {
            var result = ApplicationDbContext.Classes.FirstOrDefault(d => d.Code == code);
            return result;
        }
    }
}
