using asd123.Model;
using asd123.Presenters.Major;
using asd123.Presenters.Subject;
using AutoMapper;

namespace asd123.Helpers.DataRequestMapping
{
    public class SubjectMapping : Profile
    {
        public SubjectMapping()
        {
            CreateMap<CreateSubjectPresenter, Subject>();
            CreateMap<UpdateSubjectPresenter, Subject>();

        }
    }
}
