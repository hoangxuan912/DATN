using asd123.Model;
using asd123.Presenters.Student;
using AutoMapper;

namespace asd123.Helpers.DataRequestMapping;

public class student_mapping : Profile
{
    public student_mapping()
    {
        CreateMap<create_student_presenter, Students>();
    }
}