using asd123.Model;
using asd123.Presenters.Department;
using asd123.Presenters.Major;
using AutoMapper;

namespace asd123.Helpers.DataRequestMapping
{
    public class MajorMapping : Profile
    {
        public MajorMapping()
        {
            CreateMap<CreateMajorPresenter, Major>();

        }
    }
}
