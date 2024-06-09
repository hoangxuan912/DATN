using asd123.Model;

namespace asd123.Services
{
    public interface IMajor : IBaseService<Major>
    {
        Major GetCodeMajor(string code);
    }
    public class MajorService : BaseService<Major, ApplicationDbContext>, IMajor
    {
        public readonly ApplicationDbContext ApplicationDbContext;
        public MajorService(ApplicationDbContext context) : base(context)
        {
            ApplicationDbContext = context;
        }

        public Major GetCodeMajor(string code)
        {
            var result = ApplicationDbContext.Majors.FirstOrDefault(d => d.Code == code);
            return result ?? new Major { Code = "DefaultCode", Name = "DefaultName" };
        }

    }
}
