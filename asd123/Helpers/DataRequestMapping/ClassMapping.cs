using asd123.Model;
using asd123.Presenters.Class;
using AutoMapper;

namespace asd123.Helpers.DataRequestMapping
{
    public class ClassMapping : Profile
    {
        public ClassMapping()
        {
            CreateMap<CreateClassPresenter, Class>();
            CreateMap<UpdateClassPresenter, Class>();
        }

    }
}
