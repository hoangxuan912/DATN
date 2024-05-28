using asd123.Model;

namespace asd123.Services
{
    public interface IDepartmentService : IBaseService<Department>
    {
        Department GetCodeDepartment(string code);
    }

    public class DepartmentService : BaseService<Department, ApplicationDbContext>, IDepartmentService
    {
        public readonly ApplicationDbContext ApplicationDbContext;
        public DepartmentService(ApplicationDbContext context) : base(context)
        {
            ApplicationDbContext = context;
        }

        public Department GetCodeDepartment(string code)
        {
            var result = ApplicationDbContext.Departments.FirstOrDefault(d => d.Code == code);
            return result;
        }
    }
}
