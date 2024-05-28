using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace asd123.Helpers.DataRequestMapping
{
    public class CreateUserMapping : Profile
    {
        public CreateUserMapping()
        {
            // Default mapping when property names are same
            //CreateMap<CreateUserPresenter, UserSchema>();

            // Mapping when property names are different
            //CreateMap<User, UserViewModel>()
            //    .ForMember(dest =>
            //    dest.FName,
            //    opt => opt.MapFrom(src => src.FirstName))
            //    .ForMember(dest =>
            //    dest.LName,
            //    opt => opt.MapFrom(src => src.LastName));
        }
    }
}
