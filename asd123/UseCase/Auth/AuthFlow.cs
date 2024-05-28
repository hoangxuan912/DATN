using asd123.Helpers;
using asd123.Model;
using asd123.Services;
using asd123.Ultil;
using System.IdentityModel.Tokens.Jwt;

namespace asd123.UseCase.Auth
{
    public class AuthFlow
    {
        private readonly IUnitOfWork uow;
        public AuthFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        //public Response Login(string username, string password, byte[] secretKey)
        //{
        //    List<ApplicationUser> users = uow.Users.FindByName(username);

        //    ApplicationUser user = users.FirstOrDefault();
        //    if (user == null)
        //    {
        //        return new Response(Message.ERROR, new { });
        //    }
        //    bool isMatched = JwtUtil.Compare(password, user.Password);
        //    if (!isMatched)
        //    {
        //        return new Response(Message.ERROR, new { });
        //    }
        //    string accessToken = JwtUtil.GenerateAccessToken(user.Id, secretKey);
        //    string refreshToken = JwtUtil.GenerateRefreshToken();
        //    uow.Users.SetRefreshToken(refreshToken, user.Id);
        //    uow.Users.UpdateLoginTime(user.Id);

        //    return new Response(Message.SUCCESS, new { AccessToken = accessToken, RefreshToken = refreshToken, User = user });
        //}

        //public Response RefreshToken(string accessToken, string refreshToken, byte[] secretKey)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var jwtToken = tokenHandler.ReadJwtToken(accessToken);
        //    var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
        //    int userId = Int32.Parse(userCredentialString);
        //    applicationUser user = uow.Users.FindOne(userId);
        //    bool isMatched = user.HashRefreshToken.Equals(refreshToken);
        //    if (isMatched)
        //    {
        //        var newToken = JwtUtil.GenerateAccessToken(userId, secretKey);
        //        var newRefreshToken = JwtUtil.GenerateRefreshToken();

        //        return new Response(Message.SUCCESS, new
        //        {
        //            AccessToken = newToken,
        //            RefreshToken = newRefreshToken
        //        });
        //    }
        //    return new Response(Message.ERROR, new { });
        //}
    }
}
