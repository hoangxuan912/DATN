using Microsoft.AspNetCore.Identity;

namespace asd123.Model
{
    public class User : IdentityUser
    {

        /***Propreties***/
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
