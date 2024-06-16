using asd123.Model;
using asd123.Presenters.Mark;
using AutoMapper;

namespace asd123.Helpers.DataRequestMapping;

public class mark_mapping : Profile
{
    public mark_mapping()
    {
        CreateMap<create_mark_presenter, Marks>();
        CreateMap<update_mark_presenter, Marks>();
    }
}