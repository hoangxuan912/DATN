using asd123.Model;
using asd123.Presenters.Department;
using AutoMapper;

namespace asd123.Helpers.DataRequestMapping
{
    public class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<CreateDepartmentRequest, Department>();

        }

    }
}
